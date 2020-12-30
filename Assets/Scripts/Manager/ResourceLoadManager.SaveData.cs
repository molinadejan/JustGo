using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public partial class ResourceLoadManager
{
    private const int TOTAL_STAGE_COUNT = 24;

    private List<StageData> stageDatas = new List<StageData>();
    public List<StageData> StageDatas => stageDatas;

    public void SetStageDatas(int stageNum, bool clear, bool command, bool survive)
    {
        stageDatas[stageNum - 1].SetData(clear, command, survive);
    }

    private string jsonString;
    public string JsonString
    {
        get
        {
            return jsonString;
        }

        set
        {
            jsonString = value;
        }
    }

    private string dirPath  = Application.persistentDataPath + "/SavedData";
    private string filePath = Application.persistentDataPath + "/SavedData/save.json";

    private void CheckDataOnStart()
    {
        Debug.Log(Application.persistentDataPath);

        // 저장 디렉토리가 없을 경우 생성해준다.
        if(!Directory.Exists(dirPath))
        {
            Debug.Log("No Directory : Create new Directory");
            Directory.CreateDirectory(dirPath);
        }
        else
        {
            Debug.Log("Found Directory : Directory Exist");
        }

        // 저장 파일이 없을 경우 새로 생성해준다.
        if(!File.Exists(filePath))
        {
            Debug.Log("No Data : Create New Data");

            for (int i = 0; i < TOTAL_STAGE_COUNT; i++)
                stageDatas.Add(new StageData());

            SaveDataToLocal();
        }
        // 저장 파일이 있을 경우 불러온다.
        else
        {
            Debug.Log("Found Data : Load Data from save.json");

            string data = LoadDataFromLocal();
            stageDatas = JsonConvert.DeserializeObject<List<StageData>>(data);
        }
    }

    public void SaveDataToLocal()
    {
        string str = JsonConvert.SerializeObject(stageDatas);

        FileStream file;

        if (File.Exists(filePath))
        {
            //Debug.Log("file Exist");
            file = new FileStream(filePath, FileMode.Truncate, FileAccess.Write);
        }
        else
        {
            //Debug.Log("file Not Exist");
            file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        }

        StreamWriter sw = new StreamWriter(file);
        sw.WriteLine(str);

        sw.Close();
        file.Close();
    }

    private string LoadDataFromLocal()
    {
        string ret = null;

        FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        StreamReader sr = new StreamReader(file);
        ret = sr.ReadLine();

        sr.Close();
        file.Close();

        return ret;
    }

    public void UpdateStageData(int stageNum, bool clear, bool command, bool survive)
    {
        StageData sData = stageDatas[stageNum - 1];
        sData.SetData(clear, command, survive);
    }
}
