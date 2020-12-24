using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public Image[] stars;

    public void SetStar(int mask)
    {
        for(int i = 0; i < 3; i++)
        {
            if ((mask & (int)Mathf.Pow(2, i)) != 0)
                stars[i].sprite = ResourceLoadManager.Instance.GetSprite("StarOn");
            else
                stars[i].sprite = ResourceLoadManager.Instance.GetSprite("StarOff");
        }
    }
}
