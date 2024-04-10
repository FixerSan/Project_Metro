using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find($"@{typeof(T).Name}");
                if (go == null)
                    go = new GameObject($"@{typeof(T).Name}");

                instance = go.GetOrAddComponent<T>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
}