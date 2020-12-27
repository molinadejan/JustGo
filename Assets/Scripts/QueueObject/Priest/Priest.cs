using System.Collections;

public partial class Priest : MovingObject
{
    public int commandLimit;

    protected override void Awake()
    {
        base.Awake();
        isOver = true;
    }

    public override IEnumerator CheckPeakCor()
    {
        // Peak에 죽음
        if (TilemapManager.Instance.IsOnPeak(transform.position))
        {
            isOver = true;
            gameObject.SetActive(false);
            ResourceLoadManager.Instance.ShowBlood(transform);
        }
        // GoldChest가 현재 Position cellPos에 있는지 확인
        else
        {
            TilemapManager.Instance.IsOnChest(transform.position);
        }

        yield return null;
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        ArrowUI.Instance.ArrowUIEnable(this);
    }
}
