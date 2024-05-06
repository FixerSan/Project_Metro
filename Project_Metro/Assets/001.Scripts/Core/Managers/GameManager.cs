using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public Transform respawnTrans;
    

    public void Awake()
    {
        GameStart();
    }

    public void GameStart()
    {
        Managers.Resource.LoadAllAsync<Object>("default", _completeCallback: () => 
        {
            Managers.Data.LoadData(() =>
            {
                SetPlayerData();
                Managers.Scene.LoadScene(Define.Scene.Test, _loadCallback:() => 
                {
                    GameObject.Find("@TestController").GetComponent<TestController>().Init();   
                });
            });
        });
    }

    public void SetPlayerData()
    {
        player = new Player();
        player.status = JsonUtility.FromJson<ActorStatus>(Managers.Data.playerData.statusJson);
        player.level = JsonUtility.FromJson<PlayerLevel>(Managers.Data.playerData.levelJson);
    }

    public void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            Managers.Data.SaveData();
        }
    }

    public void OnApplicationQuit()
    {
        Managers.Data.SaveData();
    }
}
[System.Serializable]
public class Player
{
    public ActorStatus status;
    public PlayerLevel level;

    public void RespawnPlayer()
    {
        Managers.Object.player.transform.position = Managers.Game.respawnTrans.position;
    }

    public void LevelUpPlayer()
    {
        Managers.Object.InitPlayerAction();
    }
}

[SerializeField]
public class PlayerLevel
{
    public int moveLevel = 1;
    public int jumpLevel = 1;
    public int fallLevel = 1;
    public int attackLevel = 1;
    public int dashLevel = 1;
    public int defenseLevel = 1;
}
