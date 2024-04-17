using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager
{
    #region 오브젝트 참조
    public PlayerController _player;
    public HashSet<MonsterController> _monsters = new HashSet<MonsterController>();
    public Dictionary<int, Obstacle> obstacles = new Dictionary<int, Obstacle>();
    public Dictionary<int, Trigger> triggers = new Dictionary<int, Trigger>();

    #endregion
    #region Trans
    private Transform monsterTrans;
    public Transform MonsterTrans
    {
        get
        {
            if(monsterTrans == null)
            {
                GameObject go = GameObject.Find("@Monster");
                if (go == null)
                    go = new GameObject { name = "@Monster" };
                monsterTrans = go.transform;
            }
            return monsterTrans;
        }

    }

    private Transform obstacleTrans;
    public Transform ObstacleTrans
    {
        get
        {
            if (obstacleTrans == null)
            {
                GameObject go = GameObject.Find("@Obstacle");
                if (go == null)
                    go = new GameObject { name = "@Obstacle" };
                obstacleTrans = go.transform;
            }
            return obstacleTrans;
        }

    }

    private Transform triggerTrans;
    public Transform TriggerTrans
    {
        get
        {
            if (triggerTrans == null)
            {
                GameObject go = GameObject.Find("@Trigger");
                if (go == null)
                    go = new GameObject { name = "@Trigger" };
                triggerTrans = go.transform;
            }
            return triggerTrans;
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

    public Obstacle SpawnObstacle(int _index, Vector2 _position)
    {
        GameObject go = Managers.Resource.Instantiate($"Obstacle_{_index}");
        go.transform.SetParent(ObstacleTrans);
        go.transform.position = _position;
        Obstacle obstacle = go.GetComponent<Obstacle>();
        obstacles.Add(obstacle.index, obstacle);

        return obstacle;
    }

    public Trigger SpawnTrigger(int _index, Vector2 _position)
    {
        GameObject go = Managers.Resource.Instantiate($"Trigger_{_index}");
        go.transform.SetParent(TriggerTrans);
        go.transform.position = _position;
        Trigger trigger = go.GetComponent<Trigger>();
        triggers.Add(trigger.index, trigger);

        return trigger;
    }
}
