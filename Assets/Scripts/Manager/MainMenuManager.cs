using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager instance = null;

    public static MainMenuManager Instance => instance;

    [SerializeField] private UnityEvent loginSuccess;
    public void LoginSuccess() => loginSuccess?.Invoke();

    [SerializeField] private UnityEvent loginFail;
    public void LoginFail() => loginFail?.Invoke();

    [SerializeField] private UnityEvent loadSuccess;
    public void LoadSuccess() => loadSuccess?.Invoke();

    [SerializeField] private UnityEvent loadFail;
    public void LoadFail() => loadFail?.Invoke();

    [SerializeField] private List<StageSelect> stageSelects;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void OpenStageSelect()
    {
        bool check = true;

        for(int i = 0; i < stageSelects.Count; i++)
        {
            StageData data = ResourceLoadManager.Instance.StageDatas[i];
            stageSelects[i].SetStar(check, data.isClear, data.isCommand, data.isSurvive);

            check = data.isClear;
        }
    }
}
