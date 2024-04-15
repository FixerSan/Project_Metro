using JetBrains.Annotations;
using PlayerMovement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerController : Actor
{
    public PlayerData data;
    public PlayerMove move;
    public PlayerJump jump;
    public PlayerFall fall;
    public PlayerAttack attack;

    public Dictionary<Define.PlayerState, State<PlayerController>> states = new Dictionary<Define.PlayerState, State<PlayerController>>();
    public StateMachine<PlayerController> fsm;

    private Define.PlayerState currentState;
    public Define.PlayerState CurrentState;

    public Rigidbody2D rb;
    public Animator anim;

    public Transform groundCheckTrans;
    public Transform leftAttackTrans;
    public Transform rightAttackTrans;
    public Transform upAttackTrans;
    public Transform downAttackTrans;

    public bool IsGround { get { return CheckIsGround(); } }

    public void Init()
    {
        move = new PlayerMovement.Moves.One(this);
        jump = new PlayerMovement.Jumps.One(this);
        fall = new PlayerMovement.Falls.One(this);
        attack = new PlayerAttacks.One(this);

        states.Add(Define.PlayerState.Idle, new PlayerState.Idle());
        states.Add(Define.PlayerState.Move, new PlayerState.Move());
        states.Add(Define.PlayerState.Jump, new PlayerState.Jump());
        states.Add(Define.PlayerState.Fall, new PlayerState.Fall());
        fsm = new StateMachine<PlayerController>(this, states[Define.PlayerState.Idle]);
        currentState = Define.PlayerState.Idle;
        CurrentState = Define.PlayerState.Idle;
        rb = GetComponent<Rigidbody2D>();

        anim = Util.FindChild<Animator>(gameObject, "Sprite", true);
        groundCheckTrans = Util.FindChild<Transform>(gameObject, "GroundCheckTrans", true);
        leftAttackTrans = Util.FindChild<Transform>(gameObject, "LeftAttackTrans", true);
        rightAttackTrans = Util.FindChild<Transform>(gameObject, "RightAttackTrans", true);
        upAttackTrans = Util.FindChild<Transform>(gameObject, "UpAttackTrans", true);
        downAttackTrans = Util.FindChild<Transform>(gameObject, "DownAttackTrans", true);

        ChangeDirection(Define.Direction.Left);

        init = true;
    }

    public void ChangeState(Define.PlayerState _state, bool changeSameState = false)
    {
        if (_state == CurrentState && !changeSameState) return;

        fsm.ChangeState(states[_state]);
        currentState = _state;
        CurrentState = _state;
    }

    public void ChangeDirection(Define.Direction _direction)
    {
        if (currentDirection == _direction) return;
        if(_direction == Define.Direction.Left) anim.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        else if (_direction == Define.Direction.Right) anim.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        currentDirection = _direction;
    }

    private void CheckChangeStateInInspector()
    {
        if(CurrentState != currentState)
            ChangeState(CurrentState, true);
    }

    private bool CheckIsGround()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(groundCheckTrans.position, groundCheckTrans.localScale.x);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if (collider2Ds[i].CompareTag("Ground"))
            {
                anim.SetBool("IsGround", true);
                return true;
            }
        }
        anim.SetBool("IsGround", false);
        return false;
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

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheckTrans.position, groundCheckTrans.localScale.x);
    }
}
