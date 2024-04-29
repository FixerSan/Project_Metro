using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : MonsterController
{
    public override bool Init(int _index)
    {
        if(!base.Init(_index)) return false;
        states.Add(Define.MonsterState.Idle, new MonsterState.TestMonster.Idle());
        states.Add(Define.MonsterState.Move, new MonsterState.TestMonster.Move());
        states.Add(Define.MonsterState.Follow, new MonsterState.TestMonster.Follow());
        states.Add(Define.MonsterState.Attack, new MonsterState.TestMonster.Attack());
        states.Add(Define.MonsterState.Death, new MonsterState.TestMonster.Death());
        fsm = new StateMachine<MonsterController>(this, states[Define.MonsterState.Idle]);

        data = Managers.Data.GetMonsterData(_index);
        move = new MonsterMovemets.Test(this);
        attack = new MonsterAttacks.Test(this);

        anim.gameObject.GetOrAddComponent<AnimationEventHandler>().animationAction += AnimationEvent;
        init = true;
        return init;
    }

    public void AnimationEvent(int i)
    {
        if(CurrentState == Define.MonsterState.Attack)
        {
            if(i == 0) attack.Attack();
            if (i == -1) ChangeState(Define.MonsterState.Idle);
        }
    }
}
