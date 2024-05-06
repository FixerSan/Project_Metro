using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ForestKnight : BossController
{
    public float createTime;

    public Dictionary<Define.BossState, State<ForestKnight>> states;
    public StateMachine<ForestKnight> fsm;

    private Define.BossState currentState;
    public Define.BossState CurrentState;

    public Transform leftWallCheckTrans;
    public Transform rightWallCheckTrans;


    public float defaultGravityScale;

    [Header("ActionThree Data")]
    public float actionThreeCooltime;
    public float actionThreeStartDelay;
    public float actionThreeRepeatDelay;

    [Header("Cooltime")]
    public float actionOneCooltime;
    public float actionTwoCooltime;

    [Header("ActionFour Data")]
    public float actionFourCooltime;
    public float actionFourDashForce;
    public float actionFourMoveDistance;


    public bool isLeftSide { get { return CheckLeftSide(); } }
    public bool isRightSide { get { return CheckRightSide(); } }

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (!base.Init()) return false;
        Managers.Boss.InitBoss(0, this);
        leftWallCheckTrans = Util.FindChild<Transform>(gameObject, "Trans_LeftWallCheck", true);
        rightWallCheckTrans = Util.FindChild<Transform>(gameObject, "Trans_RightWallCheck", true);
        return true;
    }

    public override void CreateEffect()
    {
        StartCoroutine(CreateEffectCoroutine());
    }

    public IEnumerator CreateEffectCoroutine()
    {
        yield return new WaitForSeconds(createTime);
        ChangeState(Define.BossState.Idle);
    }

    public bool CheckRightSide()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(rightWallCheckTrans.position, 0.1f);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if (collider2Ds[i].CompareTag("Wall"))
                return true;
        }
        return false;
    }

    public bool CheckLeftSide()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(leftWallCheckTrans.position, 0.1f);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if (collider2Ds[i].CompareTag("Wall"))
                return true;
        }
        return false;
    }

    public bool CheckSide(Define.Direction _direction)
    {
        if(_direction == Define.Direction.Left) return CheckLeftSide();
        else return CheckRightSide();
    }


    public void ChangeState(Define.BossState _state, bool _isCanChangeSameState = false)
    {
        if (_state == CurrentState && !_isCanChangeSameState) return;

        fsm.ChangeState(states[_state]);
        currentState = _state;
        CurrentState = _state;
    }

    public void Update()
    {
        if (!init) return;
        if (currentState == Define.BossState.Death) return;
        fsm.Update();
        CheckChangeStateInInspector();
    }

    public void CheckChangeStateInInspector()
    {
        if (CurrentState != currentState)
            ChangeState(CurrentState, true);
    }

    public virtual void FixedUpdate()
    {
        if (!init) return;
        if (currentState == Define.BossState.Death) return;
        fsm.FixedUpdate();
    }

    public void SelectAction()
    {
        List<BossAction> canActions= new List<BossAction>();
        foreach (BossAction action in bossActions.Values)
        {
            if (action.CheckAction())
                canActions.Add(action);
        }

        int randomInt = Random.Range(0, canActions.Count);
        Debug.Log($"가능한 액션의 수는 {canActions.Count} 입니다.");
        Debug.Log($"현재 선택된 보스의 패턴은 {canActions[randomInt].stateType} 입니다.");
        ChangeState(canActions[randomInt].stateType);
    }

}
