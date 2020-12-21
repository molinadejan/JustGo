using DG.Tweening;
using System.Collections;
using UnityEngine;

public delegate void MoveDele(Vector3 dir, bool check);
public delegate void CheckPeakDele();

/// <summary>
/// Queue Play시 이동하는 오브젝트 들의 최상위 클래스 입니다.
/// </summary>
public abstract class MovingObject : QueueObject
{
    // 이동과 가시위에 있는지 체크하는 델리게이트 입니다.
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

    // 기본 이동 코루틴, 함수
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

    // 가시 체크 코루틴, 함수
    public abstract IEnumerator CheckPeakCor();

    public void CheckPeak()
    {
        StartCoroutine(CheckPeakCor());
    }

    // 이동시 앞에 물체 확인
    // 이동 가능한 물체가 있을 경우 MovingObjcet의 moveDele에 Move 함수 추가
    // 이동 가능한 물체가 있을 경우 MovingObject의 checkPeakDele에 CheckPeak 함수 추가
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

    // 초기화 시 원래 위치로 일단 이동
    public override void ResetFunc()
    {
        transform.position = startPos;
    }
}
