using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class BossManager 
{
    public BossManager() 
    {
    
    }

    public void InitBoss(int _index, BossController _boss)
    {
        _boss.states = new Dictionary<Define.BossState, State<BossController>>();
        _boss.BossAction = new Dictionary<Define.BossState, BossAction>();

        switch(_index)
        {
            case 0:
                _boss.states.Add(Define.BossState.Create, new BossState.ForestKnight.Create());
                _boss.states.Add(Define.BossState.Idle, new BossState.ForestKnight.Idle());
                _boss.states.Add(Define.BossState.ActionOne, new BossState.ForestKnight.ActionOne());
                _boss.BossAction.Add(Define.BossState.ActionOne, new BossActions.ForestKnight.One(_boss));
                break;
        }

        //_boss.data = Managers.Data.GetBossData(_index);
        _boss.fsm = new StateMachine<BossController>(_boss, _boss.states[Define.BossState.Create]);
    }
}
