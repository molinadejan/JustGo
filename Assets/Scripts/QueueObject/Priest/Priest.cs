using System.Collections;

public partial class Priest : MovingObject
{
    // 클리어 조건을 확인하기 위한 isDead 변수입니다.
    // Priest의 Survive 여부를 판단합니다.
    private bool isDead;
    public bool IsDead => isDead;

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
            isDead = true;
            animator.Play("Die");
        }
        // GoldChest가 현재 Position cellPos에 있는지 확인
        else
        {
            TilemapManager.Instance.IsOnChest(transform.position);
        }

        yield return null;
    }

    public override void ResetFunc()
    {
        base.ResetFunc();
        isDead = false;
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        ArrowUI.Instance.ArrowUIEnable(this);
    }
}
