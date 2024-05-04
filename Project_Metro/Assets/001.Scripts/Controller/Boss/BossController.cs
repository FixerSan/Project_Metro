using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossController : Actor
{
    public BossData data;
    /// <summary>
    /// 0 :: º® ºÙ±â
    /// </summary>
    public Dictionary<Define.BossAction, BossAction> bossActions;


    public virtual bool Init()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = Util.FindChild<Animator>(gameObject, "Sprite", true); 
        init = true;
        return true;
    }

    public override void Death()
    {
        
    }


    public abstract void CreateEffect();

    public enum ForestKnightState
    {
        Idle, Climbing
    }
}
