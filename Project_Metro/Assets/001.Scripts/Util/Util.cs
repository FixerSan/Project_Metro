using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Transform = UnityEngine.Transform;
using Object = UnityEngine.Object;

public static class Util
{
    public static List<Timer> timers = new List<Timer>();

    public static T GetOrAddComponent<T>(GameObject _go) where T : UnityEngine.Component
    {
        T component = _go.GetComponent<T>();
        if (component == null)
            component = _go.AddComponent<T>();

        return component;
    }
    public static T GetOrAddComponent<T>(Transform _trans) where T : UnityEngine.Component
    {
        T component = _trans.GetComponent<T>();
        if (component == null)
            component = _trans.gameObject.AddComponent<T>();

        return component;
    }

    public static T FindChild<T>(GameObject _go, string _name = null, bool _recursive = false) where T : Object
    {
        if (_go == null) return null;
        if (!_recursive)
        {
            for (int i = 0; i < _go.transform.childCount; i++)
            {
                Transform transform = _go.transform.GetChild(i);
                if (string.IsNullOrEmpty(_name) || transform.name == _name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }

        else
        {
            foreach (T component in _go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(_name) || component.name == _name)
                {
                    return component;
                }
            }
        }
        return null;
    }

    public static GameObject FindChild(GameObject _go, string _name = null, bool _recursive = false)
    {
        Transform transform = FindChild<Transform>(_go, _name, _recursive);
        if (transform != null)
            return transform.gameObject;

        return null;
    }

    public static T ParseEnum<T>(string _value)
    {
        return (T)Enum.Parse(typeof(T), _value, true);
    }

    public static void AddTimer(float _time, Action _callback)
    {
        timers.Add(new Timer(_time, _callback));
    }

    public static void CheckTimers()
    {
        for (int i = timers.Count-1; i >= 0; i--)
        {
            if (timers[i].CheckTimer())
                timers.Remove(timers[i]);
        }
    }

    public static void SaveJson(string _json, string _fileName)
    {
        string path = Path.Combine(Application.dataPath, $"006.Datas/{_fileName}");

        File.WriteAllText(path, _json);

    }
}

public class Timer
{
    public float currentTime;
    public float time;
    public Action callback;

    public Timer(float _time, Action _callback) 
    {
        currentTime = 0;
        time = _time;
        callback = _callback;
    }

    public bool CheckTimer()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= time)
        {
            callback?.Invoke();
            callback = null;
            currentTime = 0;
            time = 0;
            return true;
        }
        return false;
    }
}