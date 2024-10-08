using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager 
{
    public PlayerData playerData;
    public GameData gameData;
    public Dictionary<int, MonsterData> monsterDatas = new Dictionary<int, MonsterData>();
    public Dictionary<int, BossData> bossDatas = new Dictionary<int, BossData>();
    public Dictionary<int, DialogData> dialogDatas = new Dictionary<int, DialogData>();


    #region Save
    public void SaveData()
    {
        SaveGameData();
        SavePlayerData();
    }

    public void SavePlayerData()
    {
        Managers.Game.player.level.dashLevel = 0;
        Managers.Game.player.status.currentHP = 5;
        Managers.Game.player.status.currentATB = 0;
        PlayerData data = new PlayerData();
        data.statusJson = JsonUtility.ToJson(Managers.Game.player.status, true);
        data.levelJson = JsonUtility.ToJson(Managers.Game.player.level, true);
        data.respawnPos = Managers.Game.respawnPos;

        string dataJson = JsonUtility.ToJson(data, true);

        Util.SaveJson(dataJson, "PlayerData.Json");
    }

    public void SaveGameData()
    {
        GameData gameData = new GameData(Managers.Scene.CurrentScene, Managers.Game.respawnPos, Managers.Game.player.status.currentHP);
        string dataJson = JsonUtility.ToJson(gameData, true);

        Util.SaveJson(dataJson, "GameData.Json");
    }

    #endregion
    #region Load
    public void LoadData(Action _callback)
    {
        LoadPlayerData();
        LoadMonsterData();
        LoadDialogData();
        _callback?.Invoke();
    }

    public void LoadPlayerData()
    {
        string playerDataJson = Managers.Resource.Load<TextAsset>("PlayerData").text;
        playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);
    }

    public void LoadMonsterData()
    {
        string monsterDataJson = Managers.Resource.Load<TextAsset>("MonsterData").text;
        MonsterDatas monsterDataArray = JsonUtility.FromJson<MonsterDatas>(monsterDataJson);
        for (int i = 0; i < monsterDataArray.monsterDatas.Length; i++)
            monsterDatas.Add(monsterDataArray.monsterDatas[i].index, monsterDataArray.monsterDatas[i]);
    }

    public void LoadDialogData()
    {
        string dialogDataJson = Managers.Resource.Load<TextAsset>("DialogData").text;
        DialogDatas dialogDataArray = JsonUtility.FromJson<DialogDatas>(dialogDataJson);
        for (int i = 0; i < dialogDataArray.datas.Length; i++)
            dialogDatas.Add(dialogDataArray.datas[i].index, dialogDataArray.datas[i]);
    }

    public void LoadGameData()
    {

    }
    #endregion
    #region Get
    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public MonsterData GetMonsterData(int _index)
    {
        if (monsterDatas.TryGetValue(_index, out MonsterData data)) return data;
        Debug.Log($"인덱스: {_index}의 몬스터 데이터가 없습니다.");
        return null;
    }

    public BossData GetBossData(int _index)
    {
        if (bossDatas.TryGetValue(_index, out BossData data)) return data;
        Debug.Log($"인덱스: {_index}의 보스 데이터가 없습니다.");
        return null;
    }

    public DialogData GetDialogData(int _index)
    {
        if (dialogDatas.TryGetValue(_index, out DialogData data)) return data;
        Debug.Log($"인덱스: {_index}의 다이얼로그 데이터가 없습니다.");
        return null;
    }
    #endregion
}

[System.Serializable]
public class PlayerData
{
    public string statusJson;
    public string levelJson;
    public Vector3 respawnPos;
}

[System.Serializable]
public class MonsterDatas
{
    public MonsterData[] monsterDatas;
}

[System.Serializable]
public class MonsterData
{
    public int index;
    public float canAttackDistance;
}

[System.Serializable]
public class BossData
{
    public int index;
}

[System.Serializable]
public class DialogData
{
    public int index;
    public int nextIndex;
    public string name;
    public string sentence;
    public string buttonString;
}

[System.Serializable]
public class DialogDatas
{
    public DialogData[] datas;
}