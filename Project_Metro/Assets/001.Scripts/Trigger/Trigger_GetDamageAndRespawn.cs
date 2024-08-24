using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_GetDamageAndRespawn : Trigger_PlayerEnter
{
    public int damageForce;
    protected override void TriggerEffect(Collider2D collision)
    {
        Managers.Object.playerController.Hit(damageForce);
        Managers.Game.player.RespawnPlayer();

    }
}
