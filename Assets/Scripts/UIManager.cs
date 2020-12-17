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
    [SerializeField] private Image[] arrowImages;
    [SerializeField] private GameObject ArrowUI;

    private Priest curSelectPriest = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void SetArrowList(List<Vector2> list)
    {
        for(int i = 0; i < arrowImages.Length; i++)
        {
            if (i < list.Count)
            {
                arrowImages[i].gameObject.SetActive(true);

                if (list[i] == Vector2.up)
                {
                    arrowImages[i].sprite = arrowUp;
                }
                else if (list[i] == Vector2.down)
                {
                    arrowImages[i].sprite = arrowDown;
                }
                else if (list[i] == Vector2.left)
                {
                    arrowImages[i].sprite = arrowLeft;
                }
                else // list[i] == Vecot2.right
                {
                    arrowImages[i].sprite = arrowRight;
                }
            }
            else
            {
                arrowImages[i].gameObject.SetActive(false);
            }
        }
    }

    public void AddArrow(string dir)
    {
        int index = curSelectPriest.DirList.Count;

        if (index >= arrowImages.Length) return;

        arrowImages[index].gameObject.SetActive(true);

        if (dir.Equals("up"))
        {
            arrowImages[index].sprite = arrowUp;
            curSelectPriest.DirList.Add(Vector3.up);
        }
        else if(dir.Equals("down"))
        {
            arrowImages[index].sprite = arrowDown;
            curSelectPriest.DirList.Add(Vector3.down);
        }
        else if(dir.Equals("left"))
        {
            arrowImages[index].sprite = arrowLeft;
            curSelectPriest.DirList.Add(Vector3.left);
        }
        else // dir.Equals("right")
        {
            arrowImages[index].sprite = arrowRight;
            curSelectPriest.DirList.Add(Vector3.right);
        }
    }

    public void ArrowUIEnable(Priest priest)
    {
        if (curSelectPriest != priest)
        {
            curSelectPriest = priest;
            SetArrowList(priest.DirList);
            ArrowUI.SetActive(true);
        }
    }

    public void ArrowUIDisable()
    {
        curSelectPriest = null;
        ArrowUI.SetActive(false);
    }
}
