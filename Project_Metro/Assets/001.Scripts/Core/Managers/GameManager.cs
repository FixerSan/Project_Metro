using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool init = false;
    public Player player;
    public Transform respawnTrans;
    public Vector3 respawnPos;
    
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

    public void SaveGame(Vector3 _savePointPos)
    {
        player.save.Save(() => 
        {
            respawnPos = _savePointPos; 
            Managers.Data.SaveData();
        });
    }


    public void PlayerDead()
    {
        Managers.Input.SetCanControl(false);
        Util.AddTimer(0.5f, () =>
        {
            Managers.Screen.ShakeCamera(3, 3);
            Managers.Screen.CameraController.SetOffset(Vector3.up, 3);
            Managers.Screen.CameraController.TweeningCameraSize(Managers.Screen.CameraController.defaultCameraSize - 2, 3, () =>
            {
                Managers.Screen.ShakeCamera(3, 0.5f);
                Managers.Screen.CameraController.TweeningCameraSize(Managers.Screen.CameraController.defaultCameraSize + 1, 0.25f, () =>
                {
                    Managers.Screen.CameraController.InitCameraSize(0.5f);
                });

                Util.AddTimer(1f, () => 
                {
                    Managers.Screen.FadeIn(1f, () =>
                    {
                        PlayerRespawn();
                        Util.AddTimer(1f, () => 
                        {
                            Managers.Screen.FadeOut(1f);
                        });
                    });                
                });
            });
        });
    }

    public void PlayerRespawn()
    {
        Managers.Input.SetCanControl(true);
        Managers.Object.DespawnPlayerController();
        Managers.Object.SpawnPlayerController(respawnPos);
        Managers.Game.player.status.currentHP = Managers.Game.player.status.CurrentMaxHP;
        Managers.Screen.CameraController.SetOffset(Managers.Screen.CameraController.defaultOffset); 
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
        Managers.Object.playerController.rb.velocity = new Vector2(Managers.Object.playerController.rb.velocity.x, Managers.Game.player.status.CurrentSpeed); ;
        Managers.Screen.FadeIn(0.35f, () => 
        {
            Managers.Object.playerController.transform.position = Managers.Game.respawnTrans.position;
            Managers.Screen.FadeOut(0.35f);
        }); 
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
    public Vector3 savePointIndex;
    public Define.Scene scene;
    public int savePlayerHP;
    public GameData(Define.Scene _scene, Vector3 _respawnPos, int _savePlayerHP)
    {
        scene = _scene;
        savePointIndex = _respawnPos;
        savePlayerHP = _savePlayerHP;
    }
}
