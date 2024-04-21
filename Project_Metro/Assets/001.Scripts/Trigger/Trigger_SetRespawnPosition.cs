using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_SetRespawnPosition : Trigger_PlayerEnter
{
    protected override void TriggerEffect(Collider2D collision)
    {
        Managers.Game.respawnTrans = gameObject.transform;
    }
}
