using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Rendering;

public class UIManager 
{
    private int order = 10;                                     // 그려지는 순서 여유 선언
    private int toastOrder = 500;                               // 인스턴트 메세지 그려지는 여유 선언

    public UIScene SceneUI { get { return sceneUI; } }          // SceneUI 프로퍼티 선언
    public Dictionary<Define.UIType, UIPopup> activePopups = new Dictionary<Define.UIType, UIPopup>();

    private Stack<UIPopup> popupStack = new Stack<UIPopup>();   // 팝업 스택
    private Queue<UIToast> toastQueue = new Queue<UIToast>();   // 인스턴트 메세지 스택

    private EventSystem eventSystem = null;                     // 이벤트 시스템 선언
    private UIScene sceneUI = null;                             // SceneUI 선언

    private CanvasGroup blackPanel;
    public CanvasGroup BlackPanel
    {
        get
        {
            if (blackPanel == null)
            {
                GameObject go = GameObject.Find("@BlackPanel");
                if(go == null)
                {
                    go = Managers.Resource.Instantiate("BlackPanel");
                    go.name = "@BlackPanel";
                    UnityEngine.Object.DontDestroyOnLoad(go);
                    blackPanel = go.GetOrAddComponent<CanvasGroup>();
                }
            }
            return blackPanel;
        }
    }

    // UI 위치 선언
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" };
            }
            return root;
        }
    }                                   




    // 이벤트 시스템 설정
    public void SetEventSystem()
    {
        GameObject es = Managers.Resource.Instantiate("EventSystem");
        eventSystem = es.GetOrAddComponent<EventSystem>();
    }

    public void SetCanvas(GameObject _go, bool _sort = true, int _sortOrder = 0, bool _isToast = false)
    {
        GameObject go = GameObject.Find("EventSystem");
        if (go == null) SetEventSystem();
        Canvas canvas = _go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvas.overrideSorting = true;

        CanvasScaler cs = _go.GetOrAddComponent<CanvasScaler>();
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.referenceResolution = new Vector2(1920, 1080);

        _go.GetOrAddComponent<GraphicRaycaster>();

        if (_sort)
        {
            canvas.sortingOrder = order;
            order++;
        }
        else
        {
            canvas.sortingOrder = _sortOrder;
        }
        if (_isToast)
        {
            toastOrder++;
            canvas.sortingOrder = toastOrder;
        }
    }

    // SceneUI 생성
    public T ShowSceneUI<T>(string _name = null) where T : UIScene
    {
        if(string.IsNullOrEmpty(_name))
        {
            _name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"{_name}");
        
        T _sceneUI = go.GetOrAddComponent<T>();
        sceneUI = _sceneUI;

        go.transform.SetParent(Root.transform);

        return _sceneUI;
    }

    public void ClearScene(UIScene _scene)
    {
        sceneUI = null;
    }

    // 팝업 생성
    public T ShowPopupUI<T>(string _name = null, bool _pooling = false) where T : UIPopup
    {
        if (string.IsNullOrEmpty(_name))
        {
            _name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"{_name}",_pooling:_pooling);
        if (_pooling) Managers.Pool.CreatePool(go);
        T popup = go.GetOrAddComponent<T>();
        popupStack.Push(popup);
        activePopups.Add(Util.ParseEnum<Define.UIType>(_name), popup);
        go.transform.SetParent(Root.transform);
        return popup;
    }


    // 팝업 삭제체크
    public void ClosePopupUI(UIPopup _popup)
    {
        if (popupStack.Count == 0)
            return;

        if(popupStack.Peek() != _popup)
        {
            Debug.Log("Close Popup Failed");
            return;
        }

        ClosePopupUI(); 
    }

    // 팝업 삭제 기능
    private void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;

        UIPopup popup = popupStack.Pop();
        if(activePopups.ContainsKey(Util.ParseEnum<Define.UIType>(popup.GetType().Name)))
            activePopups.Remove(Util.ParseEnum<Define.UIType>(popup.GetType().Name));
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        order--;
    }

    // 팝업 전부 삭제
    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
        {
            ClosePopupUI();
        }
    }

    // 인스턴트 메세지 생성
    public UIToast ShowToast(string _description)
    {
        string name = nameof(UIToast);
        GameObject go = Managers.Resource.Instantiate($"{name}", _pooling: true);
        UIToast popup = go.GetOrAddComponent<UIToast>();
        popup.SetInfo(_description);
        toastQueue.Enqueue(popup);
        go.transform.SetParent(Root.transform);
        return popup;
    }

    // 인스턴트 메세지 삭제 기능
    public void CloseToastUI()
    {
        if(toastQueue.Count == 0)
        {
            return;
        }

        UIToast toast = toastQueue.Dequeue();
        toast.Refresh();
        Managers.Resource.Destroy(toast.gameObject);
        toast = null;
        toastOrder--;
    }

    // 팝업 전부 삭제
    public void CloseAllToastUI()
    {
        while (toastQueue.Count > 0)
        {
            Managers.Resource.Destroy(toastQueue.Dequeue().gameObject);
        }
    }

    // 팝업 카운트 반환
    public int GetPopupCount()
    {
        return popupStack.Count;
    }


    // 초기화
    public void Clear()
    {
        CloseAllPopupUI();
        
        Time.timeScale = 1;
        sceneUI = null;
    }
}
