using System;
using UnityEngine;

public class SceneManager
{
    private Transform sceneTrans;
    public Transform SceneTrans
    {
        get
        {
            if (sceneTrans == null)
            {
                GameObject go = GameObject.Find("@SceneTrans");
                if (go == null)
                    go = new GameObject(name: "@SceneTrans");
                sceneTrans = go.transform;
                UnityEngine.Object.DontDestroyOnLoad(go);
            }
            return sceneTrans;
        }
    }
    private Define.Scene currentScene;
    public Define.Scene CurrentScene
    {
        get { return currentScene; }
    }
    private bool isLoading = false;
    private Action loadCallback;

    public void LoadScene(Define.Scene _scene, bool _isHaveFade = true, Action _loadCallback = null)
    {
        if (!_isHaveFade)
        {
            if (isLoading) return;
            isLoading = true;
            loadCallback = _loadCallback;
            Managers.Pool.Clear();
            Managers.UI.CloseAllPopupUI();

            RemoveScene(currentScene, () =>
            {
                currentScene = _scene;
                AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync($"{_scene}");
                async.completed += (_) =>
                {
                    AddScene(_scene, () => { loadCallback?.Invoke(); });
                    isLoading = false;
                };
            });
            return;
        }

        Managers.Screen.FadeIn(0.25f, () =>
        {
            if (isLoading) return;
            isLoading = true;
            loadCallback = _loadCallback;

            Managers.Pool.Clear();
            Managers.UI.CloseAllPopupUI();
            RemoveScene(currentScene, () =>
            {
                currentScene = _scene;
                AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync($"{_scene}");
                async.completed += (_) =>                                       
                {
                    AddScene(_scene, () =>
                    {
                        isLoading = false; loadCallback?.Invoke();
                        Managers.Screen.FadeOut(0.25f);
                    });
                };
            });
        });
    }

    public void RemoveScene(Define.Scene _scene, Action _callback = null)
    {
        SceneBase bs = null;
        switch (_scene)
        {
            case Define.Scene.Scene_Test:
                bs = SceneTrans.GetComponent<Scene_Test>();
                break;

            case Define.Scene.Scene_TestBoss:
                bs = SceneTrans.GetComponent<Scene_TestBoss>();
                break;

            case Define.Scene.Scene_DarkForest:
                bs = SceneTrans.GetComponent<Scene_DarkForest>();
                break;

            default:
                _callback?.Invoke();
                return;
        }

        if (bs != null)
        {
            bs?.Clear();
            UnityEngine.Object.Destroy(bs);
        }
        _callback?.Invoke();
    }

    // ?? ???
    public void AddScene(Define.Scene _scene, Action _addSceneCallback)
    {
        SceneBase bs = null;
        //Managers.Data.LoadSceneData(addScene);
        switch (_scene)
        {
            //case Define.Scene.Stage:
            //    bs = SceneTrans.gameObject.AddComponent<StageScene>();
            //    break;

            case Define.Scene.Scene_Test:
                bs = SceneTrans.gameObject.AddComponent<Scene_Test>();
                break;

            case Define.Scene.Scene_TestBoss:
                bs = SceneTrans.gameObject.AddComponent<Scene_TestBoss>();
                break;

            case Define.Scene.Scene_DarkForest:
                bs = SceneTrans.gameObject.AddComponent<Scene_DarkForest>();
                break;
        }

        bs?.Init();
        _addSceneCallback?.Invoke();
    }

    public T GetActiveScene<T>() where T : SceneBase
    {
        return SceneTrans.GetComponent<T>() as T;
    }
}