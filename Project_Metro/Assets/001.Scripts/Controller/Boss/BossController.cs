using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossController : Actor
{
    public BossData data;
    /// <summary>
    /// 0 :: º® ºÙ±â
    /// </summary>
    public Dictionary<Define.BossState, BossAction> BossAction;

    public Dictionary<Define.BossState, State<BossController>> states;
    public StateMachine<BossController> fsm;

    private Define.BossState currentState;
    public Define.BossState CurrentState;

    public Rigidbody2D rb;
    public Animator anim;

    public virtual bool Init()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = Util.FindChild<Animator>(gameObject, "Sprite", true); 
        init = true;
        return true;
    }

    public void ChangeState(Define.BossState _state, bool _isCanChangeSameState = false)
    {
        if (_state == CurrentState && !_isCanChangeSameState) return;

        fsm.ChangeState(states[_state]);
        currentState = _state;
        CurrentState = _state;
    }

    public override void Death()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        if (!init) return;
        if (currentState == Define.BossState.Death) return;
        fsm.Update();
        CheckChangeStateInInspector();
    }

    public void FixedUpdate()
    {
        if (!init) return;
        if (currentState == Define.BossState.Death) return;
        fsm.FixedUpdate();
    }

    private void CheckChangeStateInInspector()
    {
        if (CurrentState != currentState)
            ChangeState(CurrentState, true);
    }

    public abstract void CreateEffect();

    public enum ForestKnightState
    {
        Idle, Climbing
    }
}
