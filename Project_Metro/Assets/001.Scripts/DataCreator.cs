using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataCreator : MonoBehaviour
{
    public string fileName;
    public DialogDatas data;
    public TextAsset text;

    [ContextMenu("CreateData")]
    public void CreateData()
    {
        string dataJson = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.dataPath, $"006.Datas/{fileName}.json");

        File.WriteAllText(path, dataJson);
    }

    [ContextMenu("LoadData")]
    public void LoadData()
    {
        data = JsonUtility.FromJson<DialogDatas>(text.text);
    }

}
