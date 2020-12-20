using UnityEngine;

public class HighlightUI : MonoBehaviour
{
    #region Singleton
    private static HighlightUI instance = null;

    public static HighlightUI Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<HighlightUI>();

            return instance;
        }
    }
    #endregion

    [SerializeField] private GameObject highlight;

    public void HightlightUIEnable(QueueObject obj)
    {
        highlight.SetActive(true);
        highlight.transform.position = obj.transform.position;
        highlight.transform.SetParent(obj.transform);
    }

    public void HighlightUIDisable()
    {
        highlight.SetActive(false);
        highlight.transform.position = Vector3.zero;
        highlight.transform.SetParent(null);
    }
}
