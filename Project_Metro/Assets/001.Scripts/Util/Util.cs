using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Transform = UnityEngine.Transform;
using Object = UnityEngine.Object;

public static class Util
{
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

    public static void FadeOutSpriteRenderer(SpriteRenderer _spriteRenderer, float _fadeOutTime)
    {
        Managers.Instance.StartCoroutine(FadeOutSpriteRendererRoutine(_spriteRenderer, _fadeOutTime));
    }

    private static IEnumerator FadeOutSpriteRendererRoutine(SpriteRenderer _spriteRenderer, float _fadeOutTime)
    {
        while (_spriteRenderer != null || _spriteRenderer.color.a > 0)
        {
            yield return null;
            _spriteRenderer.color -= new Color(0, 0, 0, _spriteRenderer.color.a - Time.deltaTime);
        }
    }
}