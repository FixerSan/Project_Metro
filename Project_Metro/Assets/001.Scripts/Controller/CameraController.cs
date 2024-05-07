using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public Vector2 min, max;

    public void Awake()
    {
        Managers.Screen.SetCamera(this);
    }

    void Update()
    {
        if (target == null) return;
        transform.position = target.position + offset;
    }
}
