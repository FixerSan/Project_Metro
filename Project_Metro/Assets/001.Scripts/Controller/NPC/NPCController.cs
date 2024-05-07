using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public NPC npc;


    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        npc = new NPCs.Test.One(this);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && Managers.Input.MoveAxis.y > 0)
        {
            npc.Interaction();
        }
    }
}
