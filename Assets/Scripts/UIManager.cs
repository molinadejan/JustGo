using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private UIManager instance = null;

    public static UIManager Instance { get { return Instance; }}

    public GameObject content;
    public Image[] arrowImages;

    private Priest curSelectPriest = null;
    public void SetPriest(Priest priest) => curSelectPriest = priest;


    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    public void AddArrow(Sprite arrow)
    {

    }
}
