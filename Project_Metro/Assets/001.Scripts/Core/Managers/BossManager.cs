using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.Rendering;
using UnityEngine;

public class BossManager 
{
    public BossManager() 
    {
    
    }

    public void InitBoss(int _index, BossController _boss)
    {
        switch(_index)
        {
            case 0:
                ForestKnight _forestKight = _boss as ForestKnight;
                _forestKight.states = new Dictionary<Define.BossState, State<ForestKnight>>();
                _forestKight.states.Add(Define.BossState.Create, new BossState.ForestKnights.Create());
                _forestKight.states.Add(Define.BossState.Idle, new BossState.ForestKnights.Idle());
                _forestKight.states.Add(Define.BossState.ActionOne, new BossState.ForestKnights.ActionOne());
                _forestKight.states.Add(Define.BossState.ActionTwo, new BossState.ForestKnights.ActionTwo());
                _forestKight.states.Add(Define.BossState.ActionThree, new BossState.ForestKnights.ActionThree());
                _forestKight.states.Add(Define.BossState.ActionFour, new BossState.ForestKnights.ActionFour());

                _forestKight.bossActions = new Dictionary<Define.BossAction, BossAction>();
                _forestKight.bossActions.Add(Define.BossAction.ActionOne, new BossActions.ForestKnights.Climbing(_forestKight, Define.BossState.ActionOne));
                _forestKight.bossActions.Add(Define.BossAction.ActionTwo, new BossActions.ForestKnights.JumpAndDownAttack(_forestKight, Define.BossState.ActionTwo));
                _forestKight.bossActions.Add(Define.BossAction.ActionThree, new BossActions.ForestKnights.Three(_forestKight, Define.BossState.ActionThree));
                _forestKight.bossActions.Add(Define.BossAction.ActionFour, new BossActions.ForestKnights.Four(_forestKight, Define.BossState.ActionFour));
                _forestKight.fsm = new StateMachine<ForestKnight>(_forestKight, _forestKight.states[Define.BossState.Create]);
                break;
        }

        //_boss.data = Managers.Data.GetBossData(_index);

        _boss.Init();
    }
}
