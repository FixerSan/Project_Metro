using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_ChangeCameraOffset : Trigger_PlayerEnter
{
    public Collider2D coll;

    protected override void TriggerEffect(Collider2D collision)
    {
        Managers.Screen.CameraController.ChangeCanMoveOffset(coll);
    }
}
