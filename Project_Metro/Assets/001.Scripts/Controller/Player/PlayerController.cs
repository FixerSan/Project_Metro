using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerController : Actor
{
    public Player Player { get { return Managers.Game.player; } }
    public PlayerData Data { get { return Player.data; } }
    public PlayerMove Move { get { return Player.move; } }
    public PlayerJump Jump { get { return Player.jump; } }
    public PlayerFall Fall { get { return Player.fall; } }
    public PlayerAttack Attack { get { return Player.attack; } }
    public PlayerDash Dash { get { return Player.dash; } }
    public PlayerDefence Defence { get { return Player.defence; } }
    public PlayerHeal Heal { get { return Player.heal; } }
    public PlayerSave Save { get { return Player.save; } }
    public PlayerClimb Climb { get { return Player.climb; } } 


    public Dictionary<Define.PlayerState, State<PlayerController>> states;
    public StateMachine<PlayerController> fsm;

    private Define.PlayerState currentState;
    public Define.PlayerState CurrentState;

    public Transform leftAttackTrans;
    public Transform rightAttackTrans;
    public Transform upAttackTrans;
    public Transform downAttackTrans;

    public float attackKnockbackForce;
    public float downAttackKnockbackForce;
    public float attackCountInitTime;

    public float hitKnockbackForce;
    public float hitTime;
    public float hitDuration;

    public float needLandTime;

    public float attackShakeIntensity;
    public float attackShakeTime;

    public bool isCanHit = true;

    public void Init()
    {
        #region 외부 참조
        rb = GetComponent<Rigidbody2D>();
        sr = Util.FindChild<SpriteRenderer>(gameObject, "Sprite", true);
        anim = Util.FindChild<Animator>(gameObject, "Sprite", true);
        groundCheckTrans = Util.FindChild<Transform>(gameObject, "GroundCheckTrans", true);
        leftAttackTrans = Util.FindChild<Transform>(gameObject, "LeftAttackTrans", true);
        rightAttackTrans = Util.FindChild<Transform>(gameObject, "RightAttackTrans", true);
        upAttackTrans = Util.FindChild<Transform>(gameObject, "UpAttackTrans", true);
        downAttackTrans = Util.FindChild<Transform>(gameObject, "DownAttackTrans", true);
        status = Managers.Game.player.status;
        #endregion

        #region 생성 참조
        states = new Dictionary<Define.PlayerState, State<PlayerController>>();
        states.Add(Define.PlayerState.Idle, new PlayerState.Idle());
        states.Add(Define.PlayerState.Move, new PlayerState.Move());
        states.Add(Define.PlayerState.Jump, new PlayerState.Jump());
        states.Add(Define.PlayerState.Fall, new PlayerState.Fall());
        states.Add(Define.PlayerState.Dash, new PlayerState.Dash());
        states.Add(Define.PlayerState.Defence, new PlayerState.Defence());
        states.Add(Define.PlayerState.Heal, new PlayerState.Heal()) ;
        states.Add(Define.PlayerState.Hit, new PlayerState.Hit()) ;
        states.Add(Define.PlayerState.Save, new PlayerState.Save()) ;
        states.Add(Define.PlayerState.Climb, new PlayerState.Climb());
        states.Add(Define.PlayerState.Die, new PlayerState.Die());
        fsm = new StateMachine<PlayerController>(this, states[Define.PlayerState.Idle]);
        #endregion

        #region 기타 처리
        currentState = Define.PlayerState.Idle;
        CurrentState = Define.PlayerState.Idle;
        ChangeDirection(Define.Direction.Left);
        #endregion

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
        Managers.Screen.CameraController.Flip(currentDirection);
    }

    private void CheckChangeStateInInspector()
    {
        if(CurrentState != currentState)
            ChangeState(CurrentState, true);
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

    public override void Hit(int _damage, Actor _attacker)
    {
        if (!isCanHit) return; 
        if(CurrentState == Define.PlayerState.Defence)
        {
            base.Hit(_damage - Defence.defenceForce, _attacker);
            return;
        }
        isCanHit = false;
        base.Hit(_damage, _attacker);
        Managers.UI.SceneUI?.RedrawUI();
        if (CurrentState == Define.PlayerState.Die) return;
        Managers.Screen.TweeningCameraSize(Managers.Screen.CameraController.defaultCameraSize - 0.5f, 0.1f,() => Managers.Screen.InitCameraSize(0.1f));
        //세이브 종료 
        if (Save.saveCoroutine != null)
            StopCoroutine(Save.saveCoroutine);
        ChangeState(Define.PlayerState.Hit);
        HitEffect(_attacker);
    }

    public void Hit(int _damage)
    {
        if (status.currentHP <= 0) return;
        status.currentHP -= _damage;
        StartCoroutine(HitEffect());
        if (status.currentHP <= 0)
            Death();
        Managers.Screen.TweeningCameraSize(Managers.Screen.CameraController.defaultCameraSize - 0.5f, 0.1f, () => Managers.Screen.InitCameraSize(0.1f));
        Managers.Screen.ShakeCamera(3, 0.35f);
        Managers.UI.SceneUI?.RedrawUI();

    }

    public void HitEffect(Actor _attacker)
    {
        HitKnockback(_attacker);
        Util.AddTimer(hitTime, () =>
        {
            ChangeState(Define.PlayerState.Idle);
        });

        Util.AddTimer(hitDuration, () => 
        {
            isCanHit = true;
        });
    }

    private void HitKnockback(Actor _attacker)
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

        rb.velocity = Vector2.zero;
        rb.AddForce(knockbackDir, ForceMode2D.Impulse);
    }

    public override void Death()
    {
        ChangeState(Define.PlayerState.Die);
    }

    public void DeathEffect()
    {
        SetGravity(0);
        Managers.Game.PlayerDead();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheckTrans.position, groundCheckTrans.localScale.x);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(leftAttackTrans.position, leftAttackTrans.localScale.x);
        Gizmos.DrawWireSphere(rightAttackTrans.position, rightAttackTrans.localScale.x);
    }

    public override void GetEatenSoul()
    {
        throw new System.NotImplementedException();
    }
}
