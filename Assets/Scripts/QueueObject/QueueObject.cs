using System.Collections;
using UnityEngine;

/// <summary>
/// QueuePlay시 동작할 오브젝트들의 최상위 클래스 입니다.
/// </summary>
public abstract class QueueObject : MonoBehaviour
{
    public delegate void ResetDele();

    // 초기화 델리게이트
    private static ResetDele resetDele;
    public static void InvokeResetDele() => resetDele.Invoke();

    // Queue Play시 동작할지 안 할지 체크하는 변수
    protected bool isOver;
    public bool IsOver => isOver;

    protected Animator animator;
    protected BoxCollider2D col;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// 시작할때 resetDele에 ResetFunc()를 추가합니다.
    /// </summary>
    public virtual void Start()
    {
        resetDele += ResetFunc;
    }

    /// <summary>
    /// 마우스 클릭 시 동작 정의
    /// </summary>
    protected virtual void OnMouseUp()
    {
        HighlightUI.Instance.HightlightUIEnable(this);
    }

    /// <summary>
    /// Queue Play시 실행될 QueueObject의 동작
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerator PlayOneTurnAction();

    /// <summary>
    /// 초기상태로 변경
    /// </summary>
    public abstract void ResetFunc();
}
