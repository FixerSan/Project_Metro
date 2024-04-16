using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;

public class DataManager 
{
    public Dictionary<int, PlayerData> playerDatas = new Dictionary<int, PlayerData>();

    public void LoadPlayerData()
    {

    }

    public PlayerData GetPlayerData(int _index)
    {
        if(playerDatas.TryGetValue(_index, out PlayerData data)) return data;
        return null;
    }

    public void SaveData()
    {

    }
}

[System.Serializable]
public class PlayerData
{
    public int index;
}

[System.Serializable]
public class MonsterData
{
    public int index;
    public float canAttackDistance;
}
