using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Sigleton
    private static UIManager instance = null;
    public static UIManager Instance => instance;
    #endregion

    [SerializeField] private Sprite arrowUp;
    [SerializeField] private Sprite arrowDown;
    [SerializeField] private Sprite arrowLeft;
    [SerializeField] private Sprite arrowRight;

    // ArrowList의 Arrow 이미지
    // 활성화/비활성화로 조정
    // 최대 20개까지 설정 가능
    [SerializeField] private List<Arrow> arrows;
    [SerializeField] private GameObject arrowUI;
    [SerializeField] private Scrollbar arrowScrollBar;

    private Priest curSelectPriest = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        Input.multiTouchEnabled = false;
    }

    private void SetArrowList(List<Vector2> list)
    {
        for(int i = 0; i < arrows.Count; i++)
        {
            if (i < list.Count)
            {
                arrows[i].gameObject.SetActive(true);

                if (list[i] == Vector2.up)
                {
                    arrows[i].SetSprite(arrowUp);
                }
                else if (list[i] == Vector2.down)
                {
                    arrows[i].SetSprite(arrowDown);
                }
                else if (list[i] == Vector2.left)
                {
                    arrows[i].SetSprite(arrowLeft);
                }
                else // list[i] == Vecot2.right
                {
                    arrows[i].SetSprite(arrowRight);
                }

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

        if (dir.Equals("up"))
        {
            arrows[index].SetSprite(arrowUp);
            curSelectPriest.DirList.Add(Vector3.up);
        }
        else if(dir.Equals("down"))
        {
            arrows[index].SetSprite(arrowDown);
            curSelectPriest.DirList.Add(Vector3.down);
        }
        else if(dir.Equals("left"))
        {
            arrows[index].SetSprite(arrowLeft);
            curSelectPriest.DirList.Add(Vector3.left);
        }
        else // dir.Equals("right")
        {
            arrows[index].SetSprite(arrowRight);
            curSelectPriest.DirList.Add(Vector3.right);
        }

        arrows[index].SetText((index + 1).ToString());

        arrowScrollBar.value = 1f;
    }

    public void MinusArrow(Arrow arrow)
    {
        int index = arrows.IndexOf(arrow);

        curSelectPriest.DirList.RemoveAt(index);
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
