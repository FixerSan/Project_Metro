using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pool
{
    private GameObject prefab; // 사용할 프리팹
    private Queue<GameObject> poolObjectQueue; // 게임 오브젝트를 재활용할 큐
    private Transform transform_Pool; // 풀의 부모 트랜스폼
    private string poolName; // 풀의 이름

    // 초기화
    public Pool(GameObject _prefab, string _poolName)
    {
        prefab = _prefab;
        poolName = _poolName;
        poolObjectQueue = new Queue<GameObject>();
        Init(); 
    }

    // 게임오브젝트 서치 및 위치 선언
    private void Init()
    {
        GameObject go = GameObject.Find("@Pool");
        if (go == null)
        {
            go = new GameObject { name = "@Pool" };
        }
        GameObject _transform_Pool = new GameObject { name = poolName };
        _transform_Pool.transform.SetParent(go.transform);
        transform_Pool = _transform_Pool.transform;
    }

    // 게임 오브젝트 반환
    public GameObject Get()
    {
        GameObject poolObject;
        if (poolObjectQueue.TryDequeue(out GameObject _poolObject))
        {
            poolObject = _poolObject;
        }

        else
        {
            poolObject = GameObject.Instantiate(prefab);
            poolObject.name = prefab.name;
        }

        poolObject.SetActive(true); 
        return poolObject;
    }

    // 게임 오브젝트 푸쉬
    public void Push(GameObject _poolObject)
    {
        _poolObject.transform.SetParent(transform_Pool);
        _poolObject.SetActive(false); 
        poolObjectQueue.Enqueue(_poolObject); 
    }

    // 풀 초기화
    public void Clear()
    {
        poolObjectQueue.Clear();
    }
}

public class PoolManager 
{
    public Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();    // 풀 딕셔너리

    // 모든 풀 클리어
    public void Clear()
    {
        foreach (var item in poolDictionary)
        {
            poolDictionary[item.Key].Clear();
        }
        poolDictionary.Clear(); 
    }

    // 게임 오브젝트를 풀에서 가져옴
    public GameObject Get(GameObject _poolingObject)
    {
        if (!poolDictionary.ContainsKey(_poolingObject.name))
            CreatePool(_poolingObject);

        return poolDictionary[_poolingObject.name].Get(); 
    }

    // 게임 오브젝트 푸쉬
    public bool Push(GameObject _go)
    {
        if (poolDictionary.ContainsKey(_go.name))
        {
            poolDictionary[_go.name].Push(_go);
            return true;
        }
        return false;
    }

    // 특정 프리팹의 풀 존재 여부 확인
    public bool CheckExist(GameObject _prefab)
    {
        string key = _prefab.name;
        return poolDictionary.ContainsKey(key);
    }

    // 풀 생성 및 초기화
    public void CreatePool(GameObject _prefab, System.Action _callback = null)
    {
        string key = _prefab.name;
        if (poolDictionary.ContainsKey(key))
            return;
        Pool pool = new Pool(_prefab, $"{key} Pool"); 
        poolDictionary.Add(key, pool); 
        _callback?.Invoke();
    }

    // 특정 풀 삭제
    public void DeletePool(string _key)
    {
        if (poolDictionary.ContainsKey(_key))
        {
            poolDictionary[_key].Clear(); 
            poolDictionary.Remove(_key); 
        }
    }

    public void Init()
    {
        Clear();
    }
}
