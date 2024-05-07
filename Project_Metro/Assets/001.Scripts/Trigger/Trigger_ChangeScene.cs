using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Trigger_ChangeScene : Trigger_PlayerEnter
{
    public Define.Scene scene;
    protected override void TriggerEffect(Collider2D collision)
    {
        Managers.Scene.LoadScene(scene);
    }
}
