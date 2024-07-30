using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager
{
    public Action callback;
    public DialogData currentData;

    public void Call(int _index, Action _callback = null)
    {
        Managers.Input.SetCanControl(false);
        currentData = Managers.Data.GetDialogData(_index);
        Managers.UI.ShowDialogUI(currentData.index);
        callback = _callback;
    }

    public void CheckEvent(Action<bool> _callback)
    {
        if (currentData.nextIndex == -2)
        {
            Managers.Routine.StartCoroutine(EventCallRoutine());
            _callback.Invoke(true);
        }

        else
            _callback.Invoke(false);
    }

    private IEnumerator EventCallRoutine()
    {
        yield return new WaitForSeconds(1);
        Managers.Event.DialogEvent(currentData.index);
        EndDialog();
    }

    public void OnClick_NextButton()
    {
        if (currentData.nextIndex == -1)
        {
            EndDialog();
        }

        else if (currentData.nextIndex != -2)
        {
            Call(currentData.nextIndex, callback);
        }
    }

    public void EndDialog()
    {
        Managers.UI.CloseDialogUI();
        Managers.Routine.StartCoroutine(EndDialogRoutine());

        if (callback != null)
        {
            callback.Invoke();
            callback = null;
        }
    }

    private IEnumerator EndDialogRoutine()
    {
        yield return new WaitForSeconds(1);
        Managers.Input.SetCanControl(true);
    }
}
