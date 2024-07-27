using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave
{
    public PlayerController controller;
    public Coroutine saveCoroutine;

    public PlayerSave(PlayerController _controller)
    {
        controller = _controller;
    }

    public void Save(System.Action _callback)
    {
        controller.ChangeState(Define.PlayerState.Save);
        saveCoroutine = controller.StartCoroutine(SaveRoutine(_callback));
    }

    public IEnumerator SaveRoutine(System.Action _callback)
    {
        //�ִϸ��̼� �ð� ���� ����
        yield return new WaitForSeconds(1);
        _callback?.Invoke();
        controller.ChangeState(Define.PlayerState.Idle);
    }
}
