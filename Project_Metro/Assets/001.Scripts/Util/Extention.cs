using System;
using System.Collections.Generic;
using UnityEngine;


public static class Extension
{
    #region GameObject
    public static T GetOrAddComponent<T>(this GameObject _go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(_go);
    }

    public static T GetOrAddComponent<T>(this Transform _transform) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(_transform);
    }

    //public static void BindEvent(this GameObject _go, Action _eventCallback = null, Action<BaseEventData> _dragEventCallback = null, Action<BaseEventData> _dropEventCallback = null, Define.UIEventType _eventType = Define.UIEventType.Click)
    //{
    //    UIBase.BindEvent(_go, _eventCallback, _dragEventCallback, _dropEventCallback, _eventType);
    //}

    #endregion
    #region Array and List
    public static T Random<T>(this List<T> _list)
    {
        int random = UnityEngine.Random.Range(0, _list.Count);
        return _list[random];
    }

    public static T Random<T>(this T[] _array)
    {
        int random = UnityEngine.Random.Range(0, _array.Length);
        return _array[random];
    }

    public static T TryGetValue<T>(this List<T> _list, int _index) where T : class
    {
        if (_index < 0 || _index >= _list.Count)
            return null;
        return _list[_index];
    }

    public static T TryGetValue<T>(this T[] _array, int _index) where T : class
    {
        if (_index < 0 || _index >= _array.Length)
            return null;
        return _array[_index];
    }

    public static T FindEmpty<T>(this T[] _array) where T : class
    {
        for (int i = 0; i < _array.Length; i++)
        {
            if (_array[i] == null) return _array[i];
        }
        return null;
    }

    public static int NullCount<T>(this T[] _array) where T : class
    {
        int count = 0;
        for (int i = 0; i < _array.Length; i++)
        {
            if (_array[i] == null)
                count++;
        }
        return count;
    }

    public static int NotNullCount<T>(this T[] _array) where T : class
    {
        int count = 0;
        for (int i = 0; i < _array.Length; i++)
        {
            if (_array[i] != null)
                count++;
        }
        return count;
    }

    public static List<T> ArrayToList<T>(this T[] _array) where T : class
    {
        List<T> list = new List<T>();
        for (int i = 0; i < _array.Length; i++)
        {
            if (_array[i] != null)
                list.Add(_array[i]);
        }
        return list;
    }
    #endregion
}