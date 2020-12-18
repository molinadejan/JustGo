using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : QueueObject
{
    public float moveAmount;
    public float moveTime;
    public Ease ease;

    [SerializeField] private List<Vector3> dirList = new List<Vector3>();
    public List<Vector3> DirList => dirList;
    public void AddDirList(string dir)
    {
        if      (dir.Equals("up"))    dirList.Add(Vector3.up);
        else if (dir.Equals("down"))  dirList.Add(Vector3.down);
        else if (dir.Equals("left"))  dirList.Add(Vector3.left);
        else if (dir.Equals("right")) dirList.Add(Vector3.right);
    }

    private Vector3 startPos;
    private int listIndex = 0;
    private bool checkMove;

    private Animator animator;

    private WaitForSeconds waitForSeconds;
    private WaitUntil waitUntil;

    private void Awake()
    {
        startPos = transform.position;
        animator = GetComponent<Animator>();

        waitForSeconds = new WaitForSeconds(0.25f);
        waitUntil = new WaitUntil(() => checkMove);
    }

    // 이동 코루틴
    private IEnumerator MoveCor(Vector3 dir)
    {
        checkMove = false;

        if (dir == Vector3.zero)
        {
            checkMove = true;
            yield return null;
        }

        transform.DOMove(transform.position + dir * moveAmount, moveTime).SetEase(ease)
            .OnComplete(() => { checkMove = true; });

        yield return waitUntil;
        yield return waitForSeconds;
        yield return null;
    }

    // 가시 체크 코루틴
    private IEnumerator CheckPeakCor(Vector3 position)
    {
        if(PeakManager.Instance.IsOnPeak(position))
        {
            SetDie();
            yield return waitForSeconds;
        }

        yield return null;
    }

    private Vector3 GetNextDir()
    {
        if (dirList.Count == 0)
        {
            isOver = true;
            return Vector3.zero;
        }

        Vector3 ret = dirList[listIndex++];

        if (listIndex >= dirList.Count) isOver = true;

        return ret;
    }

    private void OnMouseUp() => UIManager.Instance.ArrowUIEnable(this);

    private bool isDead = false;

    private void SetDie()
    {
        isOver = true;
        animator.SetTrigger("Die");

        isDead = true;
    }

    public override void ResetFunc()
    {
        isOver = false;

        if(dirList.Count > 0 && isDead) animator.SetTrigger("Idle");

        transform.position = startPos;
        listIndex = 0;

        isDead = false;
    }

    public override IEnumerator PlayOneTurnAction()
    {
        yield return StartCoroutine(MoveCor(GetNextDir()));
        yield return StartCoroutine(CheckPeakCor(transform.position));

        yield return null;
    }
}
