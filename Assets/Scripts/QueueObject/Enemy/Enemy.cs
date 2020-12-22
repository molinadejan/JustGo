using System.Collections;
using UnityEngine;

public abstract partial class Enemy : MovingObject
{
    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < directionList.Count; i++)
            AddDirList(directionList[i].ToString());
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

    public override void ResetFunc()
    {
        base.ResetFunc();
        isOver = false;
        animator.Play("Idle");
        listIndex = 0;
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        ArrowUI.Instance.ArrowUIEnable(this);
    }
}
