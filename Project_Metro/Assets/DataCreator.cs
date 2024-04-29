using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataCreator : MonoBehaviour
{
    public string fileName;
    public MonsterDatas data;

    [ContextMenu("CreateData")]
    public void CreateData()
    {
        string dataJson = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.dataPath, $"006.Datas/{fileName}.json");

        File.WriteAllText(path, dataJson);
    }
}
