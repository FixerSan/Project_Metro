using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public PlayerController SpawnPlayerController()
    {
        
        return _player;
    }
}
