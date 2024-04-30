using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager 
{
    public PlayerData playerData;
    public Dictionary<int, MonsterData> monsterDatas = new Dictionary<int, MonsterData>();


    #region Save
    public void SaveData()
    {
        SavePlayerData();
    }

    public void SavePlayerData()
    {
        PlayerData data = new PlayerData();
        data.statusJson = JsonUtility.ToJson(Managers.Game.player.status, true);
        data.levelJson = JsonUtility.ToJson(Managers.Game.player.level, true);

        string dataJson = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.dataPath, "006.Datas/PlayerData.json");

        File.WriteAllText(path, dataJson);
    }
    #endregion
    #region Load
    public void LoadData(Action _callback)
    {
        LoadPlayerData();
        LoadMonsterData();
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
    #endregion
}

[System.Serializable]
public class PlayerData
{
    public string statusJson;
    public string levelJson;
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
