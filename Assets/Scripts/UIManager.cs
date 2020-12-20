using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Sigleton
    private static UIManager instance = null;
    public static UIManager Instance => instance;
    #endregion

    [SerializeField] private GameObject highlight;

    [SerializeField] private ArrowUI arrowUI;
    [SerializeField] private ResultUI resultUI;

    public void GameClear(bool commands, bool survive)
    {
        resultUI.ShowResult(commands, survive);
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void SetHightlight(QueueObject obj)
    {
        highlight.SetActive(true);
        highlight.transform.position = obj.transform.position;
        highlight.transform.SetParent(obj.transform);
    }
}
