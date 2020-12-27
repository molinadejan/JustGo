using DG.Tweening;

// ArrowUI의 활성화 비활성화를 담당하는 ArrowUI partial class

public partial class ArrowUI
{
    /// <summary>
    /// ArrowUI에 Arrow를 활성화합니다.
    /// </summary>
    /// <param name="dir">Arrow의 방향</param>
    public void AddArrow(string dir)
    {
        int index = curSelectPriest.DirList.Count;

        if (index >= arrows.Count || index >= curSelectPriest.commandLimit) return;

        arrows[index].gameObject.SetActive(true);
        arrows[index].SetSprite(dir);
        arrows[index].SetText((index + 1).ToString());

        curSelectPriest.AddDirList(dir);

        // arrow scrollbar의 value = 1 Tweening
        DOTween.To(() => arrowScrollbar.value, x => arrowScrollbar.value = x, 1f, 0.1f);
    }

    /// <summary>
    /// ArrowUI에 Arrow를 비활성화 합니다.
    /// </summary>
    /// <param name="arrow">비활성화할 Arrow</param>
    public void MinusArrow(Arrow arrow)
    {
        if (curSelectPriest == null) return;

        int index = arrows.IndexOf(arrow);

        curSelectPriest.RemoveAtDirList(index);
        SetArrowList(curSelectPriest.DirList);
    }
}
