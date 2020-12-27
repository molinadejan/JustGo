// ArrowUI의 Arrow 추가, 제거를 담당하는 ArrowUI partial class
public partial class ArrowUI
{
    // ArrowUI를 활성화 합니다. 파라미터로 넘어온 Priest의 DirList를 보여줍니다.
    public void ArrowUIEnable(Priest priest)
    {
        if (curSelectPriest != priest)
        {
            curSelectPriest = priest;
            SetArrowList(priest.DirList);
            arrowUI.SetActive(true);
            arrowUIInput.SetActive(true);
            priestMaxCommand.gameObject.SetActive(true);
            priestMaxCommand.text = "MAX " + curSelectPriest.commandLimit;
        }
    }

    public void ArrowUIEnable(Enemy skeleton)
    {
        curSelectPriest = null;
        SetArrowList(skeleton.DirList);
        arrowUI.SetActive(true);
        arrowUIInput.SetActive(false);
        priestMaxCommand.gameObject.SetActive(false);
    }

    // ArrowUI를 비활성화 합니다.
    public void ArrowUIDisable()
    {
        curSelectPriest = null;
        arrowUI.SetActive(false);
    }
}
