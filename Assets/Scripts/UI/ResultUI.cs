using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour 
{
    #region Singleton
    private static ResultUI instance = null;
    public static ResultUI Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ResultUI>();

            return instance;
        }
    }
    #endregion

    [SerializeField] private GameObject resultUI;
    [SerializeField] private Image starCommand;
    [SerializeField] private Text commandText;
    [SerializeField] private Image starSurvive;
    [SerializeField] private Text surviveText;

    public void ResultUIEnable(int commandCount, bool commands, int deathCount)
    {
        resultUI.SetActive(true);

        starCommand.sprite = ResourceLoadManager.Instance.GetSprite(commands ? "starOn" : "starOff");
        commandText.text = commandCount.ToString() + " Commands";

        starSurvive.sprite = ResourceLoadManager.Instance.GetSprite(deathCount == 0 ? "starOn" : "starOff");
        surviveText.text = deathCount == 0 ? "All Survive" : deathCount + " dead";
    }

    public void ResultUIDisable()
    {
        resultUI.SetActive(false);

        starCommand.sprite = null;
        starSurvive.sprite = null;
    }
}
