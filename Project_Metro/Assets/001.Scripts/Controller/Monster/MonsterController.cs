using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterController : Actor
{
    public MonsterData data;
    public MonsterMove move;
    public MonsterAttack attack;
    public MonsterAttack attack2;
    public MonsterDie die;

    public Dictionary<Define.MonsterState, State<MonsterController>> states;
    public StateMachine<MonsterController> fsm;

    private Define.MonsterState currentState;
    public Define.MonsterState CurrentState;

    public Transform detectRangeTrans;
    public Transform attackTrans;
    public Actor  attackTarget;
    public float attackTime = 0.5f;
    public float deathTime = 1f;
    public int dropGoldValue;

    private Coroutine deathCoroutine;

    public float attackKnockbackForce;
    public float hitKnockbackForce;
    public float hitTime;

    public bool isBattle;

    public virtual bool Init()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = Util.FindChild<Animator>(gameObject, "Sprite", true);
        isBattle = false;
        status.currentHP = status.defaultMaxHP;
        return true;
    }

    public void ChangeState(Define.MonsterState _state , bool _isCanChangeSameState = false)
    {
        if (_state == CurrentState && !_isCanChangeSameState) return;

        fsm.ChangeState(states[_state]);
        currentState = _state;
        CurrentState = _state;
    }

    private void CheckChangeStateInInspector()
    {
        if (CurrentState != currentState)
            ChangeState(CurrentState, true);
    }

    public void ChangeDirection(Define.Direction _direction)
    {
        if (currentDirection == _direction) return;
        if (_direction == Define.Direction.Left) transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        else if (_direction == Define.Direction.Right) transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        currentDirection = _direction;
    }

    public void Update()
    {
        if (!init) return;
        if (currentState == Define.MonsterState.Death) return;
        fsm.Update();
        CheckChangeStateInInspector();
    }

    public void FixedUpdate()
    {
        if (!init) return;
        if (currentState == Define.MonsterState.Death) return;
        fsm.FixedUpdate();
    }

    public override void Death()
    {
        ChangeState(Define.MonsterState.Death);
    }

    public void StartBattle()
    {
        isBattle = true;
        Managers.Game.player.StartBattle();
    }
    public override void Hit(int _damage, Actor _attacker)
    {
        base.Hit(_damage, _attacker);
        attackTarget = _attacker;
        if(CurrentState != Define.MonsterState.Death)
            ChangeState(Define.MonsterState.Follow);
    
        if (KnockbackLevel <= _attacker.KnockbackLevel)
        {
            Vector2 knockbackDir = Vector2.zero;
            if (transform.position.x - _attacker.transform.position.x < 0)
            {
                ChangeDirection(Define.Direction.Right);
                knockbackDir.x = -1f * hitKnockbackForce;
            }
            else
            {
                ChangeDirection(Define.Direction.Left);
                knockbackDir.x = 1f * hitKnockbackForce;
            }
            knockbackDir.y = 1f;
            HitKnockback(knockbackDir);
        }
    }

    public void HitKnockback(Vector2 _knockbackDirection)
    {
        move.isCanMove = false;
        rb.velocity = Vector2.zero;
        rb.AddForce(_knockbackDirection, ForceMode2D.Impulse);
        StartCoroutine(HitRoutine());
    }

    public IEnumerator HitRoutine()
    {
        yield return new WaitForSeconds(hitTime);
        move.isCanMove = true;
        move.Stop();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(detectRangeTrans.position, detectRangeTrans.localScale);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackTrans.position, attackTrans.localScale);
    }
}
