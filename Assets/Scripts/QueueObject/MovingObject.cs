using DG.Tweening;
using System.Collections;
using UnityEngine;

public delegate void MoveDele(Vector3 dir, bool check);
public delegate void CheckPeakDele();

public abstract class MovingObject : QueueObject
{
    protected MoveDele moveDele;
    protected CheckPeakDele checkPeakDele;

    [SerializeField] private float moveAmount;
    [SerializeField] private float moveTime;
    [SerializeField] private Ease ease;

    private bool checkMove;
    private WaitForSeconds waitForSeconds;
    private WaitUntil waitUntil;
    private Vector3 startPos;

    protected override void Awake()
    {
        base.Awake();

        startPos = transform.position;
        waitForSeconds = new WaitForSeconds(0.25f);
        waitUntil = new WaitUntil(() => checkMove);
    }

    public IEnumerator MoveCor(Vector3 dir, bool check)
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

    public abstract IEnumerator CheckPeakCor();

    public void CheckPeak()
    {
        StartCoroutine(CheckPeakCor());
    }

    public bool CheckNext(Vector3 dir, ref MoveDele moveDele, ref CheckPeakDele checkPeakDele)
    {
        col.enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, moveAmount);
        col.enabled = true;

        if (hit && hit.transform.CompareTag("MovingObject"))
        {
            MovingObject nextMovingObject = hit.transform.GetComponent<MovingObject>();
            moveDele += nextMovingObject.Move;
            checkPeakDele += nextMovingObject.CheckPeak;

            return nextMovingObject.CheckNext(dir, ref moveDele, ref checkPeakDele);
        }
        else if (TilemapManager.Instance.IsOnWall(transform.position, dir * moveAmount))
        {
            return false;
        }

        return true;
    }

    public override void ResetFunc()
    {
        transform.position = startPos;
    }
}
