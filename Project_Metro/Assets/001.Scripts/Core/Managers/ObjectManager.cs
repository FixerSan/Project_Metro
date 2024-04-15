using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager
{
    #region 오브젝트 참조
    private PlayerController _player;
    private HashSet<MonsterController> _monsters = new HashSet<MonsterController>();
    #endregion
    #region Trans
    private Transform _monsterTrans;
    public Transform MonsterTrans
    {
        get
        {
            if(_monsterTrans == null)
            {
                _monsterTrans = GameObject.Find("@Monster").transform;
                if (_monsterTrans == null)
                    _monsterTrans = new GameObject { name = "@Monster" }.transform;
            }
            return _monsterTrans;
        }

    }
    #endregion

    public PlayerController SpawnPlayerController(Vector2 _position)
    {
        
        return _player;
    }

    public NormalAttack SpawnAttack(Actor _attacker, Transform _attackPos, Define.PlayerAttackDirection _attackDirection)
    {
        GameObject go = Managers.Resource.Instantiate($"{_attackDirection}Attack_{Managers.Game.attackLevel}", _pooling:true);
        NormalAttack attack = go.GetOrAddComponent<NormalAttack>();
        attack.Init(_attacker, _attackPos);

        return attack;
    }
}
