﻿using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour 
{
    #region Singleton
    private static ResultUI instance = null;
    public static ResultUI Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ResultUI>();

            return instance;
        }
    }
    #endregion

    [SerializeField] private GameObject resultUI;
    [SerializeField] private Image starCommand;
    [SerializeField] private Image starSurvive;

    public void ResultUIEnable(bool commands, bool survive)
    {
        resultUI.SetActive(true);

        starCommand.sprite = ResourceLoadManager.Instance.GetSprite(commands ? "starOn" : "starOff");
        starSurvive.sprite = ResourceLoadManager.Instance.GetSprite(survive ? "starOn" : "starOff");
    }

    public void ResultUIDisable()
    {
        resultUI.SetActive(false);

        starCommand.sprite = null;
        starSurvive.sprite = null;
    }
}
