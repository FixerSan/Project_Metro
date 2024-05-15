using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_CameraSet : Trigger_PlayerEnter
{
    public Vector2 min;
    public Vector2 max;
    public Vector3 offset;

    protected override void TriggerEffect(Collider2D collision)
    {
        Managers.Screen.CameraController.SetCameraRange(min, max);
        Managers.Screen.CameraController.SetCameraOffset(offset);
    }
}
