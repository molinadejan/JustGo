﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;
    public static GameManager Instance => instance;
    #endregion

    [SerializeField] private List<QueueObject> qObjects;
    [SerializeField] private int maxCommand; // for get star

    private List<Priest> priests = new List<Priest>();
    private GameObject[] chests;


    [SerializeField] private UnityEvent playEvent;
    public void InvokePlayEvent() => playEvent.Invoke();


    [SerializeField] private UnityEvent clearEvent;
    public void InvokeClearEvent() => clearEvent.Invoke();


    [SerializeField] private UnityEvent retryEvent;
    public void InvokeRetryEvent() => retryEvent.Invoke();


    private WaitForSeconds waitForSeconds;

    private bool isGameClear;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            priests.AddRange(FindObjectsOfType<Priest>());
            waitForSeconds = new WaitForSeconds(0.5f);
        }
    }

    private void Start()
    {
        Input.multiTouchEnabled = false;
        StartCoroutine(StartCor());
    }

    public IEnumerator StartCor()
    {
        yield return null;
        chests = GameObject.FindGameObjectsWithTag("GoldChest");
    }

    public void PlayQueue()
    {
        playEvent?.Invoke();
        StartCoroutine(PlayQueueCor());
    }

    private IEnumerator PlayQueueCor()
    {
        yield return waitForSeconds;

        while (!CheckPriestsAllDie() && !CheckGetAllChests())
        {
            for (int i = 0; i < qObjects.Count; i++)
            {
                if (!qObjects[i].IsOver)
                {
                    yield return StartCoroutine(qObjects[i].PlayOneTurnAction());

                    if (CheckGetAllChests()) break;
                }
            }
        }

        yield return waitForSeconds;

        if (isGameClear) clearEvent?.Invoke();
        else             retryEvent?.Invoke();
    }

    public void RetryStage()
    {
        QueueObject.InvokeResetDele();
        isGameClear = false;

        for (int i = 0; i < chests.Length; i++) chests[i].SetActive(true);
    }

    public void CheckClearStar()
    {
        int totalCommand = 0;
        bool allSurvive = true;

        foreach (Priest priest in priests)
        {
            totalCommand += priest.DirList.Count;
            allSurvive |= !priest.IsDead;
        }

        UIManager.Instance.GameClear(totalCommand <= maxCommand, allSurvive);
    }

    private bool CheckPriestsAllDie()
    {
        for (int i = 0; i < priests.Count; i++)
            if (!priests[i].IsOver) return false;

        return true;
    }

    private bool CheckGetAllChests()
    {
        for (int i = 0; i < chests.Length; i++)
            if (chests[i].activeSelf)
                return false;

        return isGameClear = true;
    }
}
