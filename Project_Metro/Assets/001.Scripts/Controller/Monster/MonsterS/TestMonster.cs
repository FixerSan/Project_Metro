using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : MonsterController
{
    public override bool Init()
    {
        if(!base.Init()) return false;
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

    public override void GetEatenSoul()
    {

    }
}
