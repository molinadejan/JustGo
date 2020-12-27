using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }
    #endregion

    [SerializeField] private List<QueueObject> qObjects;
    [SerializeField] private int maxCommand;

    private List<Priest> priests = new List<Priest>();
    private GameObject[] chests;

    [SerializeField] private UnityEvent playEvent;
    [SerializeField] private UnityEvent clearEvent;
    [SerializeField] private UnityEvent retryEvent;

    private WaitForSeconds waitForSeconds;

    private bool isGameClear;

    private void Awake()
    {
        priests.AddRange(FindObjectsOfType<Priest>());
        waitForSeconds = new WaitForSeconds(0.5f);
    }

    private void Start()
    {
        Input.multiTouchEnabled = false;

        for (int i = 0; i < qObjects.Count; i++)
            qObjects[i].order.sprite = ResourceLoadManager.Instance.GetSprite((i + 1).ToString());

        StartCoroutine(StartCor());
    }

    public IEnumerator StartCor()
    {
        yield return null;
        chests = GameObject.FindGameObjectsWithTag("GoldChest");
    }

    // 스테이지 플레이
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

                    if (CheckGetAllChests() || CheckPriestsAllDie()) break;
                }
            }
        }

        yield return waitForSeconds;

        if (isGameClear) ClearStage();
        else             RetryStage();
    }

    // 스테이지 클리어 동작
    public void ClearStage()
    {
        clearEvent?.Invoke();
        CheckClearStar();
        HighlightUI.Instance.HighlightUIDisable();
        ArrowUI.Instance.ArrowUIDisable();
    }

    // 스테이지 재시작 동작
    public void RetryStage()
    {
        retryEvent?.Invoke();

        QueueObject.InvokeResetDele();
        isGameClear = false;

        for (int i = 0; i < chests.Length; i++) chests[i].SetActive(true);

        ResultUI.Instance.ResultUIDisable();
    }

    // 클리어 별의 조건을 확인합니다.
    // 총 커맨드 수, Priest 전원 생존
    public void CheckClearStar()
    {
        int totalCommand = 0;
        int totalDeath = 0;

        foreach (Priest priest in priests)
        {
            totalCommand += priest.DirList.Count;

            if (!priest.gameObject.activeSelf) ++totalDeath;
        }

        ResultUI.Instance.ResultUIEnable(totalCommand, totalCommand <= maxCommand, totalDeath);
    }

    //Priest들이 남은 행동이 있는지 확인합니다.
    private bool CheckPriestsAllDie()
    {
        for (int i = 0; i < priests.Count; i++)
            if (!priests[i].IsOver) return false;

        return true;
    }

    //모든 GoldChest를 얻었는지 확인합니다.
    private bool CheckGetAllChests()
    {
        for (int i = 0; i < chests.Length; i++)
            if (chests[i].activeSelf)
                return false;

        return isGameClear = true;
    }
}
