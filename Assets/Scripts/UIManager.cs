using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    #region Sigleton
    private static UIManager instance = null;
    public static UIManager Instance => instance;
    #endregion

    // ArrowList의 Arrow 이미지
    // 활성화/비활성화로 조정
    // 최대 20개까지 설정 가능
    [SerializeField] private List<Arrow> arrows;
    [SerializeField] private GameObject arrowUI;
    [SerializeField] private Scrollbar arrowScrollbar;

    private Priest curSelectPriest = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

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

    public void AddArrow(string dir)
    {
        int index = curSelectPriest.DirList.Count;

        if (index >= arrows.Count) return;

        arrows[index].gameObject.SetActive(true);
        arrows[index].SetSprite(dir);
        arrows[index].SetText((index + 1).ToString());

        curSelectPriest.AddDirList(dir);

        StartCoroutine(ChangeScrollbarValue());
    }

    private IEnumerator ChangeScrollbarValue()
    {
        yield return null;
        yield return null;

        arrowScrollbar.value = 1f;
    }

    public void MinusArrow(Arrow arrow)
    {
        int index = arrows.IndexOf(arrow);

        curSelectPriest.RemoveAtDirList(index);
        SetArrowList(curSelectPriest.DirList);
    }

    public void ArrowUIEnable(Priest priest)
    {
        if (curSelectPriest != priest)
        {
            curSelectPriest = priest;
            SetArrowList(priest.DirList);
            arrowUI.SetActive(true);
        }
    }

    public void ArrowUIDisable()
    {
        curSelectPriest = null;
        arrowUI.SetActive(false);
    }
}
