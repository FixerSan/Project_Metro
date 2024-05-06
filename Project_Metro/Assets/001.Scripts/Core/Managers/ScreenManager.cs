using System;
using System.Collections;
using UnityEngine;

public class ScreenManager
{
    private CameraController cameraController;  // 카메라 컨트롤러 선언

    public CameraController CameraController    // 카메라 컨트롤러 프로퍼티 선언
    {
        get
        {
            if (cameraController == null)
                CreateCamera();

            return cameraController;
        }
    }

    private void CreateCamera()
    {
        GameObject go = GameObject.Find("Main Camera");
        if (go == null)
            go = Managers.Resource.Instantiate("Main Camera");
        UnityEngine.Object.DontDestroyOnLoad(go);
        cameraController = go.GetOrAddComponent<CameraController>();
    }

    #region Fade

    public void FadeIn(float _fadeTime, Action _callback = null)
    {
        Managers.Routine.StartCoroutine(FadeInRoutine(_fadeTime, () => { _callback?.Invoke(); }));
    }

    public void FadeOut(float _fadeTime, Action _callback = null)
    {
        Managers.Routine.StartCoroutine(FadeOutRoutine(_fadeTime, () => { _callback?.Invoke(); }));
    }

    public void FadeInOut(float _totalTile, Action _callback = null)
    {
        Managers.Routine.StartCoroutine(FadeInOutRoutine(_totalTile, () => { _callback?.Invoke(); }));
    }

    private IEnumerator FadeInRoutine(float _fadeTime, Action _callback = null)
    {
        Managers.UI.BlackPanel.gameObject.SetActive(true);
        Managers.UI.BlackPanel.alpha = 0;
        while (Managers.UI.BlackPanel.alpha < 1)
        {
            Managers.UI.BlackPanel.alpha = Managers.UI.BlackPanel.alpha + Time.deltaTime / _fadeTime;
            yield return null;
        }
        Managers.UI.BlackPanel.alpha = 1;
        _callback?.Invoke();
    }

    private IEnumerator FadeOutRoutine(float _fadeTime, Action _callback = null)
    {
        Managers.UI.BlackPanel.gameObject.SetActive(true);
        Managers.UI.BlackPanel.alpha = 1;
        while (Managers.UI.BlackPanel.alpha > 0)
        {
            Managers.UI.BlackPanel.alpha = Managers.UI.BlackPanel.alpha - Time.deltaTime / _fadeTime;
            yield return null;
        }
        Managers.UI.BlackPanel.alpha = 0;
        Managers.UI.BlackPanel.gameObject.SetActive(false);
        _callback?.Invoke();
    }

    private IEnumerator FadeInOutRoutine(float _totalTile, Action _callback = null)
    {
        yield return Managers.Routine.StartCoroutine(FadeInRoutine(_totalTile * 0.5f));
        yield return Managers.Routine.StartCoroutine(FadeOutRoutine(_totalTile * 0.5f));
        _callback?.Invoke();
    }
    #endregion
}