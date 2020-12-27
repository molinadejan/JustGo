using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    #region Variables

    private Image[] stars = new Image[3];

    private Image GetStar(int index)
    {
        if (stars[index] == null)
            stars[index] = transform.GetChild(index + 1).GetComponent<Image>();

        return stars[index];
    }

    private Button btn;
    private Button Btn
    {
        get
        {
            if (btn == null) btn = GetComponent<Button>();

            return btn;
        }
    }

    #endregion

    public void SetStar(bool interactable, bool clear, bool command, bool survive)
    {
        Btn.interactable = interactable;

        if (interactable)
        {
            GetStar(0).sprite = ResourceLoadManager.Instance.GetSprite(clear ? "starOn" : "starOff");
            GetStar(1).sprite = ResourceLoadManager.Instance.GetSprite(command ? "starOn" : "starOff");
            GetStar(2).sprite = ResourceLoadManager.Instance.GetSprite(survive ? "starOn" : "starOff");
        }
        else
        {
            GetStar(0).gameObject.SetActive(false);
            GetStar(1).gameObject.SetActive(false);
            GetStar(2).gameObject.SetActive(false);
        }
    }
}
