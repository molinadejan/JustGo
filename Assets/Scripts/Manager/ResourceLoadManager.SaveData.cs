using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public partial class ResourceLoadManager
{
    private const int TOTAL_STAGE_COUNT = 24;

    private List<StageData> stageDatas = new List<StageData>();
    public List<StageData> StageDatas => stageDatas;

    private string jsonString;
    public string JsonString
    {
        get
        {
            jsonString = JsonConvert.SerializeObject(stageDatas);
            return jsonString;
        }

        set
        {
            jsonString = value;

            // Load Error, Invoke LoadFailData
            if (jsonString.Equals("LoadError"))
            {
                Debug.Log("Load Data : Failed Load Data");
                return;
            }
            // No Data
            else if (jsonString.Length == 0)
            {
                for (int i = 0; i < TOTAL_STAGE_COUNT; i++)
                    stageDatas.Add(new StageData());

                GoogleManager.Instance.SaveCloud();

                Debug.Log("Load Data : No Data");
            }
            // There is Saved Data
            else
            {
                stageDatas = JsonConvert.DeserializeObject<List<StageData>>(jsonString);

                Debug.Log("Load Data : Yes Data");
            }
        }
    }
}
