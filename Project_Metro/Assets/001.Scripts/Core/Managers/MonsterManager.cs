using MonsterState.TestMonster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterManager 
{
    public MonsterManager()
    {

    }

    public void InitMonster(int _index, MonsterController _monster)
    {
        _monster.states = new Dictionary<Define.MonsterState, State<MonsterController>>();

        switch(_index)
        {
            case 0:
                _monster.states.Add(Define.MonsterState.Idle, new MonsterState.TestMonster.Idle());
                _monster.states.Add(Define.MonsterState.Move, new MonsterState.TestMonster.Move());
                _monster.states.Add(Define.MonsterState.Follow, new MonsterState.TestMonster.Follow());
                _monster.states.Add(Define.MonsterState.Attack, new MonsterState.TestMonster.Attack());
                _monster.states.Add(Define.MonsterState.Death, new MonsterState.TestMonster.Death());
                _monster.move = new MonsterMovemets.Test(_monster);
                _monster.attack = new MonsterAttacks.Test(_monster);
                break;
        }

        _monster.data = Managers.Data.GetMonsterData(_index);
        _monster.fsm = new StateMachine<MonsterController>(_monster, _monster.states[Define.MonsterState.Idle]);
    }
}
