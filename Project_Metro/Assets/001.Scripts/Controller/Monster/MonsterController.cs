using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterController : Actor
{
    public MonsterData data;
    public MonsterMove move;
    public MonsterAttack attack;
    public MonsterAttack attack2;

    public Dictionary<Define.MonsterState, State<MonsterController>> states;
    public StateMachine<MonsterController> fsm;

    private Define.MonsterState currentState;
    public Define.MonsterState CurrentState;

    public Rigidbody2D rb;
    public Animator anim;

    public Transform detectRangeTrans;
    public Transform attackTrans;
    public PlayerController attackTarget;
    public float attackTime = 0.5f;
    public float deathTime = 1f;

    private Coroutine deathCoroutine;

    public virtual bool Init()
    {
        states = new Dictionary<Define.MonsterState, State<MonsterController>>();
        rb = GetComponent<Rigidbody2D>();
        anim = Util.FindChild<Animator>(gameObject, "Sprite", true);
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

    public void DeathEffect()
    {
        if (deathCoroutine != null) return;
        anim.SetTrigger("Dead");
        move.Stop();
        StopAllCoroutines();
        deathCoroutine = StartCoroutine(DeathRoutine());
    }

    public IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(deathTime);
        Managers.Resource.Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(detectRangeTrans.position, detectRangeTrans.localScale);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackTrans.position, attackTrans.localScale);
    }
}
