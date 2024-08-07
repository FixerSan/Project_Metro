using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool init = false;
    public Player player;
    public Transform respawnTrans;
    public int nowSavePointIndex;
    
    public void Awake()
    {
        GameStart();
    }

    public void Update()
    {
        if (!init) return;
        Util.CheckTimers();
        player.Update();
    }

    public void GameStart()
    {
        Managers.Resource.LoadAllAsync<Object>("default", _completeCallback: () => 
        {
            Managers.Data.LoadData(() =>
            {
                SetPlayer();
                DebugSettings debugSettings = Managers.Resource.Load<GameObject>("DebugSettings").GetComponent<DebugSettings>();
                if(debugSettings.isPlayerSpawn)
                    Managers.Object.SpawnPlayerController(debugSettings.playerSpawnPosition);
                Managers.Screen.CameraController.SetTarget(Managers.Object.SpawnPlayerController(debugSettings.playerSpawnPosition).transform);
                Managers.Scene.LoadScene(debugSettings.TestScene, _loadCallback:() => 
                {
                    init = true;
                });
            });
        });
    }

    public void SetPlayer()
    {
        player = new Player();
        player.status = JsonUtility.FromJson<ActorStatus>(Managers.Data.playerData.statusJson);
        player.level = JsonUtility.FromJson<PlayerLevel>(Managers.Data.playerData.levelJson);
    }

    public void SaveGame(int _savePointIndex)
    {
        player.save.Save(() => 
        {
            nowSavePointIndex = _savePointIndex; 
            Managers.Data.SaveData();
        });
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
    public PlayerData data;
    public PlayerMove move;
    public PlayerJump jump;
    public PlayerFall fall;
    public PlayerAttack attack;
    public PlayerDash dash;
    public PlayerDefence defence;
    public PlayerHeal heal;
    public PlayerSave save;
    public PlayerClimb climb;

    public bool isBattle = false;

    private WaitForSeconds battleCheckTime = new WaitForSeconds(3);
    private Coroutine battleCoroutine;

    public int gold;

    public void Update()
    {
        CheckRegenerationATB();
    }

    public void RespawnPlayer()
    {
        Managers.Object.playerController.transform.position = Managers.Game.respawnTrans.position;
    }

    public void LevelUpPlayer()
    {
        Managers.Object.InitPlayerAction();
    }

    public void StartBattle()
    {
        isBattle = true;
        battleCoroutine = Managers.Routine.StartCoroutine(CheckBattleRoutine());
    }

    public void GetSoulSkill(Define.SoulSkill _soulSkill)
    {
        if (_soulSkill == Define.SoulSkill.VineHeart) level.vineHeartLevel = 1;
        LevelUpPlayer();
    }

    private IEnumerator CheckBattleRoutine()
    {
        while (isBattle)
        {
            yield return battleCheckTime;
            isBattle = false;
            foreach (var monster in Managers.Object.monsters)
            {
                if (isBattle) break;
                isBattle = monster.isBattle;
            }
        }

        EndBattle();
    }

    public void EndBattle()
    {
        isBattle = false;
        if (battleCoroutine == null) Managers.Routine.StopCoroutine(battleCoroutine);
        Managers.UI.SceneUI?.RedrawUI();
        Managers.Routine.StartCoroutine(EndBattleRoutine());
    }

    private IEnumerator EndBattleRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        status.currentATB = 0;
    }

    public void CheckRegenerationATB()
    {
        if (isBattle) RegenerationATB();
    }

    public void RegenerationATB()
    {
        if (status.currentATB == status.CurrentMaxATB) return;

        status.currentATB += status.defaultRegenerationATBForce * Time.deltaTime;
        if (status.currentATB > status.CurrentMaxATB)
            status.currentATB = status.CurrentMaxATB;
        Managers.UI.SceneUI?.RedrawUI();
    }

    public void GetHP(int _getHpValue)
    {
        status.currentHP += _getHpValue;
        if (status.currentHP > status.CurrentMaxHP)
            status.currentHP = status.CurrentMaxHP;
        Managers.UI.SceneUI?.RedrawUI();
    }

    public void GetGold(int _getGoldValue)
    {
        gold += _getGoldValue;
        Managers.UI.SceneUI.RedrawUI();
    }

    public void UseGold(int _useGoldValue)
    {
        gold -= _useGoldValue;
        Managers.UI.SceneUI.RedrawUI();
    }
}

[System.Serializable]
[SerializeField]
public class PlayerLevel
{
    public int moveLevel = 1;
    public int jumpLevel = 1;
    public int fallLevel = 1;
    public int attackLevel = 1;
    public int dashLevel = 1;
    public int defenseLevel = 1;
    public int vineHeartLevel = 0;
}

[System.Serializable]
public class GameData
{
    public int savePointIndex = 0;
    public int savePlayerHP;
    public GameData(int _savePointIndex, int _savePlayerHP)
    {
        savePointIndex = _savePointIndex;
        savePlayerHP = _savePlayerHP;
    }
}
