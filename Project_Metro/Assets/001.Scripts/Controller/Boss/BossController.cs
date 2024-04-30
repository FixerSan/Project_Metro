using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using static UnityEditor.VersionControl.Asset;

public abstract class BossController : Actor
{
    public BossData data;
    public List<BossAction> bossSkills;

    public Dictionary<Define.MonsterState, State<BossController>> states;
    public StateMachine<BossController> fsm;

    private Define.BossAction currentBossAction;
    public Define.BossAction CurrentBossAction;

    public Rigidbody2D rb;
    public Animator anim;

    public virtual bool Init()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = Util.FindChild<Animator>(gameObject, "Sprite", true);
        return true;
    }

    public override void Death()
    {
        throw new System.NotImplementedException();
    }
}
