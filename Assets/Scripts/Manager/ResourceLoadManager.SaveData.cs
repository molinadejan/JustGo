using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public partial class ResourceLoadManager
{
    private const int TOTAL_STAGE_COUNT = 24;

    private List<StageData> stageDatas = new List<StageData>();

    private void LoadSavedData()
    {
        TextAsset savedData = Resources.Load<TextAsset>("SavedData/data.json");

        if(savedData == null)
        {
            for(int i = 0; i < TOTAL_STAGE_COUNT; i++)
                stageDatas.Add(new StageData());

            string jsonString = JsonConvert.SerializeObject(stageDatas);
            File.WriteAllText(Application.persistentDataPath + "/data.json", jsonString);
        }
        else
        {
            Debug.Log("Yes Data!!!");
        }
    }
}
