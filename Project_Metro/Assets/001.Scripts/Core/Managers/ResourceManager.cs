using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class ResourceManager 
{
    //로드된 오브젝트들을 담아놓는 딕셔너리
    private Dictionary<string, Object> resourceDictionary = new Dictionary<string, Object>();

    //콜백 로드 호출
    public void Load<T>(string _key, Action<T> _callback = null) where T : Object
    {
        T ob = CheckLoaded<T>(_key);

        if (ob != null)
        {
            _callback?.Invoke(ob as T);
            return;
        }

        LoadAsync<T>(_key, (_ob) =>
        {
            _callback?.Invoke(_ob as T);
        });
    }

    //로드 호출
    public T Load<T>(string _key) where T:Object
    {
        return CheckLoaded<T>(_key);
    }

    //로딩창 사용 용도
    public void LoadAllAsync<T>(string _label, Action<string,int,int> _callback = null, Action _completeCallback = null) where T : Object
    {
        var operationHandle = Addressables.LoadResourceLocationsAsync(_label, typeof(T));

        operationHandle.Completed += (op) => 
        {
            int currentLoadCount = 0;
            int totalLoadCount = op.Result.Count;

            foreach (var result in op.Result)
            {
                LoadAsync<T>(result.PrimaryKey, (ob) => 
                {
                    currentLoadCount++;
                    _callback?.Invoke(result.PrimaryKey, currentLoadCount, totalLoadCount);
                    if (currentLoadCount == totalLoadCount)
                        _completeCallback?.Invoke();
                });
            }
        };
    }

    //이미 로드 된 것(딕셔너리)을 뽑아올 때
    private T CheckLoaded<T>(string _key) where T : Object
    {
        string loadKey = ChangeKey<T>(_key);

        if (resourceDictionary.TryGetValue(loadKey, out Object resource))
            return resource as T;
        return null;
    }

    //비동기 로드
    private void LoadAsync<T>(string _key, Action<T> _callback = null) where T : Object
    {
        string loadKey = ChangeKey<T>(_key);

        var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
        asyncOperation.Completed += (op) => 
        {
            if(!resourceDictionary.ContainsKey(loadKey))
                resourceDictionary.Add(loadKey, op.Result);
            _callback?.Invoke(op.Result as T);
        };
    }

    //오브젝트 생성
    public GameObject Instantiate(string _key, Transform _parent = null, bool _pooling = false)
    {
        GameObject prefab = CheckLoaded<GameObject>($"{_key}");
        if (prefab == null)
        {
            Debug.LogError($"Failed to load prefab : {_key}");
            return null;
        }

        if (_pooling)
        {
            GameObject poolingObject = Managers.Pool.Get(prefab);
            poolingObject.transform.SetParent(_parent);
            return poolingObject;
        }

        GameObject go = Object.Instantiate(prefab, _parent);

        go.name = prefab.name;
        return go;
    }

    // 오브젝트 파괴
    public void Destroy(GameObject _go)
    {
        if (_go == null) return;
        if (Managers.Pool.Push(_go)) return;

        Object.Destroy(_go);
    }

    //로드 키 변환
    private string ChangeKey<T>(string _key) where T : Object
    {
        if (typeof(T) == typeof(GameObject)) _key = _key + ".prefab";
        else if (typeof(T) == typeof(TextAsset)) _key = _key + ".Data";
        else if (typeof(T) == typeof(AudioClip)) _key = _key + ".AudioClip";
        else if (typeof(T) == typeof(Sprite)) _key = _key + ".sprite";
        else if (typeof(T) == typeof(RuntimeAnimatorController)) _key = _key + ".controller";

        return _key;
    }
}
