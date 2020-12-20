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

    public void SetHightlight(QueueObject obj)
    {
        gameObject.SetActive(true);
        transform.position = obj.transform.position;
        transform.SetParent(obj.transform);
    }

    private void OnDisable()
    {
        transform.position = Vector3.zero;
        transform.SetParent(null);
    }
}
