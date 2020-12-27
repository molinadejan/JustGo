using System;

[Serializable]
public class StageData
{
    public bool isClear;
    public bool isCommand;
    public bool isSurvive;

    public StageData() => isClear = isCommand = isSurvive = false;

    public void SetData(bool clear, bool command, bool survive)
    {
        isClear = clear;
        isCommand = command;
        isSurvive = survive;
    }
}
