using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MoveDele(Vector3 dir, bool check);
public delegate void CheckPeakDele();

public class Priest : QueueObject
{
    public float moveAmount;
    public float moveTime;
    public Ease ease;

    #region manage direction list

    [SerializeField] private List<Vector3> dirList = new List<Vector3>();
    public List<Vector3> DirList => dirList;
    public void AddDirList(string dir)
    {
        if      (dir.Equals("up"))    dirList.Add(Vector3.up);
        else if (dir.Equals("down"))  dirList.Add(Vector3.down);
        else if (dir.Equals("left"))  dirList.Add(Vector3.left);
        else if (dir.Equals("right")) dirList.Add(Vector3.right);
        else return;

        isOver = false;
    }
    public void RemoveAtDirList(int index)
    {
        dirList.RemoveAt(index);
        isOver = dirList.Count == 0 ? true : false;
    }

    #endregion

    private Vector3 startPos;
    private int listIndex = 0;
    private bool checkMove;

    private Animator animator;
    private BoxCollider2D col;

    private WaitForSeconds waitForSeconds;
    private WaitUntil waitUntil;

    private MoveDele moveDele;
    private CheckPeakDele checkPeakDele;

    private void Awake()
    {
        startPos = transform.position;
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();

        waitForSeconds = new WaitForSeconds(0.25f);
        waitUntil = new WaitUntil(() => checkMove);

        isOver = true;
    }

    // 이동 코루틴
    private IEnumerator MoveCor(Vector3 dir, bool check)
    {
        checkMove = false;

        if (check)
        {
            transform.DOMove(transform.position + dir * moveAmount, moveTime).SetEase(ease)
                .OnComplete(() => { checkMove = true; });
        }
        else
        {
            transform.DOShakePosition(moveTime, 0.2f)
                .OnComplete(() => { checkMove = true; });
        }

        yield return waitUntil;
        yield return waitForSeconds;
        yield return null;
    }

    public void Move(Vector3 dir, bool check)
    {
        StartCoroutine(MoveCor(dir, check));
    }

    private IEnumerator CheckPeakCor()
    {
        if (TilemapManager.Instance.IsOnPeak(transform.position)) SetDie();
        yield return null;
    }

    public void CheckPeak()
    {
        StartCoroutine(CheckPeakCor());
    }

    public bool CheckNext(Vector3 dir, ref MoveDele moveDele, ref CheckPeakDele checkPeakDele)
    {
        col.enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, moveAmount);
        col.enabled = true;

        if(hit && hit.transform.CompareTag("Priest"))
        {
            Priest nextPriest = hit.transform.GetComponent<Priest>();
            moveDele += nextPriest.Move;
            checkPeakDele += nextPriest.CheckPeak;

            return nextPriest.CheckNext(dir, ref moveDele, ref checkPeakDele);
        }
        else if(TilemapManager.Instance.IsOnWall(transform.position, dir * moveAmount))
        {
            return false;
        }
        
        return true;
    }

    private Vector3 GetNextDir()
    {
        Vector3 ret = dirList[listIndex++];

        if (listIndex >= dirList.Count) isOver = true;

        return ret;
    }

    private void OnMouseUp() => UIManager.Instance.ArrowUIEnable(this);

    private void SetDie()
    {
        isOver = true;
        animator.Play("Die");
    }

    public override void ResetFunc()
    {
        isOver = dirList.Count == 0 ? true : false;
        animator.Play("Idle");

        transform.position = startPos;
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
}
