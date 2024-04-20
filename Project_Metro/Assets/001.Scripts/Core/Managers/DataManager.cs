using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager 
{
    public PlayerData playerData;

    public void SavePlayerData()
    {
        PlayerData data = new PlayerData();
        data.statusJson = JsonUtility.ToJson(Managers.Game.player.status, true);
        data.levelJson = JsonUtility.ToJson(Managers.Game.player.level, true);

        string dataJson = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.dataPath, "006.Datas/PlayerData.json");

        File.WriteAllText(path, dataJson);
    }

    public void LoadPlayerData()
    {
        string playerDataJson = Managers.Resource.Load<TextAsset>("PlayerData").text;
        playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public void SaveData()
    {
        SavePlayerData();
    }

    public void LoadData(Action _callback)
    {
        LoadPlayerData();
        _callback?.Invoke();
    }
}

[System.Serializable]
public class PlayerData
{
    public string statusJson;
    public string levelJson;
}

[System.Serializable]
public class MonsterData
{
    public int index;
    public float canAttackDistance;
}
