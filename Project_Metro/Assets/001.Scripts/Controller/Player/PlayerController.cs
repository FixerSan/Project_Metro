using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Actor
{
    public PlayerData data;
    public PlayerMovement movement;

    public Dictionary<Define.PlayerState, State<PlayerController>> states = new Dictionary<Define.PlayerState, State<PlayerController>>();
    public StateMachine<PlayerController> fsm;

    private Define.PlayerState currentState;
    public Define.PlayerState CurrentState;

    public Rigidbody2D rb;
    public void Init()
    {
        movement = new PlayerMovement(this);
        states.Add(Define.PlayerState.Idle, new PlayerState.Idle());
        states.Add(Define.PlayerState.Move, new PlayerState.Move());
        fsm = new StateMachine<PlayerController>(this, states[Define.PlayerState.Idle]);
        currentState = Define.PlayerState.Idle;
        CurrentState = Define.PlayerState.Idle;
        rb = GetComponent<Rigidbody2D>();

        init = true;
    }

    public void ChangeState(Define.PlayerState _state, bool changeSameState = false)
    {
        if (_state == CurrentState && !changeSameState) return;

        fsm.ChangeState(states[_state]);
        currentState = _state;
        CurrentState = _state;
    }

    private void CheckChangeStateInInspector()
    {
        if(CurrentState != currentState)
            ChangeState(CurrentState);
    }

    public void Awake()
    {
        Init();
    }

    public void Update()
    {
        if (!init) return;
        fsm.Update();
        CheckChangeStateInInspector();
    }

    public void FixedUpdate()
    {
        if (!init) return;
        fsm.FixedUpdate();
    }

    public override void Hit(int _damage)
    {
        base.Hit(_damage);
    }

    public override void Die()
    {

    }

}
