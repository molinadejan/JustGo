using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class ArrowUI : MonoBehaviour
{
    #region Singleton
    private static ArrowUI instance = null;
    public static ArrowUI Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ArrowUI>();

            return instance;
        }
    }
    #endregion

    [SerializeField] private List<Arrow> arrows;
    [SerializeField] private GameObject arrowUI;
    [SerializeField] private GameObject arrowUIInput;
    [SerializeField] private Scrollbar arrowScrollbar;
    [SerializeField] private Text priestMaxCommand;

    private Priest curSelectPriest = null;

    private void Start()
    {
        // Arrow의 Button에 MinusArrow 메서드를 Add합니다.
        for (int i = 0; i < arrows.Count; i++)
        {
            Button btn = arrows[i].GetComponent<Button>();
            Arrow arrow = arrows[i];
            btn.onClick.AddListener(() => MinusArrow(arrow));
        }
    }

    /// <summary>
    /// list의 Vector3를 읽어와 ArrowUI에 표시합니다.
    /// </summary>
    /// <param name="list">ArrowUI에 표시할 Priest의 DirList</param>
    private void SetArrowList(List<Vector3> list)
    {
        for (int i = 0; i < arrows.Count; i++)
        {
            if (i < list.Count)
            {
                arrows[i].gameObject.SetActive(true);
                arrows[i].SetSprite(list[i]);
                arrows[i].SetText((i + 1).ToString());
            }
            else
            {
                arrows[i].gameObject.SetActive(false);
            }
        }
    }
}
