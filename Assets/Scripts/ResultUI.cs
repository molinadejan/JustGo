using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameObject resultUI;
    [SerializeField] private Image starCommand;
    [SerializeField] private Image starSurvive;

    public void ShowResult(bool commands, bool survive)
    {
        resultUI.SetActive(true);

        starCommand.sprite = SpriteManager.Instance.GetSprite(commands ? "starOn" : "starOff");
        starSurvive.sprite = SpriteManager.Instance.GetSprite(survive ? "starOn" : "starOff");
    }
}
