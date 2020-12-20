using System.Collections;
using UnityEngine;

public partial class Priest : MovingObject
{
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

        isOver = dirList.Count == 0 ? true : false;
        isDead = false;
        animator.Play("Idle");
        listIndex = 0;
    }

    public override IEnumerator PlayOneTurnAction()
    {
        Vector3 nextDir = GetNextDir();

        bool check = CheckNext(nextDir, ref moveDele, ref checkPeakDele);

        moveDele?.Invoke(nextDir, check);
        yield return StartCoroutine(MoveCor(nextDir, check));

        checkPeakDele?.Invoke();
        yield return StartCoroutine(CheckPeakCor());

        moveDele = null;
        checkPeakDele = null;

        yield return null;
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        ArrowUI.Instance.ArrowUIEnable(this);
    }
}
