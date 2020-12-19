using System.Collections;
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
    private List<Priest> priests = new List<Priest>();
    private GameObject[] chests;

    public UnityEvent playEvent;
    public UnityEvent resetEvent;

    private WaitForSeconds waitForSeconds;

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

    private IEnumerator StartCor()
    {
        yield return null;
        chests = GameObject.FindGameObjectsWithTag("GoldChest");
    }

    public void PlayQueue()
    {
        playEvent?.Invoke();
        StartCoroutine(PlayQueueCor());
    }

    public void ReSetGame()
    {
        resetEvent?.Invoke();
        QueueObject.resetDele.Invoke();

        for (int i = 0; i < chests.Length; i++) chests[i].SetActive(true);
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

                    if (CheckGetAllChests())
                        break;
                }
            }
        }

        yield return waitForSeconds;

        ReSetGame();
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

        Debug.Log("Game Clear");
        return true;
    }
}
