using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    private Image[] stars;

    private void Awake()
    {
        stars = new Image[3];

        for (int i = 0; i < 3; i++)
            stars[i] = transform.GetChild(i + 1).GetComponent<Image>();
    }

    public void SetStar(bool clear, bool command, bool survive)
    {
        stars[0].sprite = ResourceLoadManager.Instance.GetSprite(clear   ? "starOn" : "starOff");
        stars[1].sprite = ResourceLoadManager.Instance.GetSprite(command ? "starOn" : "starOff");
        stars[2].sprite = ResourceLoadManager.Instance.GetSprite(survive ? "starOn" : "starOff");
    }
}
