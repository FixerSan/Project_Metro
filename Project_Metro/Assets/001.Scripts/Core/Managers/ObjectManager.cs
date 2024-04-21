using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager
{
    #region 오브젝트 참조
    public PlayerController player;
    public CameraController camera;
    public HashSet<MonsterController> monsters = new HashSet<MonsterController>();
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
        if (player == null)
            player = Managers.Resource.Instantiate("PlayerController").GetOrAddComponent<PlayerController>();

        player.transform.position = _position;
        InitPlayerAction();
        player.Init();
        return player;
    }

    public void InitPlayerAction()
    {
        if (Managers.Game.player.level.moveLevel == 1) player.move = new PlayerMoves.One(player);

        if (Managers.Game.player.level.jumpLevel == 1) player.jump = new PlayerJumps.One(player);

        if (Managers.Game.player.level.attackLevel == 1) player.attack = new PlayerAttacks.One(player);

        if (Managers.Game.player.level.fallLevel == 1) player.fall = new PlayerFalls.One(player);

        if (Managers.Game.player.level.dashLevel == 1) player.dash = new PlayerDashes.One(player);

        if (Managers.Game.player.level.defenseLevel == 1) player.defence = new PlayerDefenses.One(player);
    }

    public NormalAttack SpawnAttack(Actor _attacker, Transform _attackPos, Define.PlayerAttackDirection _attackDirection)
    {
        GameObject go = Managers.Resource.Instantiate($"{_attackDirection}Attack_{Managers.Game.player.level.attackLevel}", _pooling:true);
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
