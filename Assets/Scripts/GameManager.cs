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
    }

    private IEnumerator PlayQueueCor()
    {
        yield return waitForSeconds;

        while (!CheckPriestsAllOver())
            for (int i = 0; i < qObjects.Count; i++)
                if(!qObjects[i].IsOver) yield return StartCoroutine(qObjects[i].PlayOneTurnAction());

        yield return waitForSeconds;

        ReSetGame();
    }

    private bool CheckPriestsAllOver()
    {
        for (int i = 0; i < priests.Count; i++)
            if (!priests[i].IsOver) return false;

        return true;
    }
}
