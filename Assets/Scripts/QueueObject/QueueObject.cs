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

    protected BoxCollider2D col;

    protected virtual void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    public virtual void Start()
    {
        resetDele += ResetFunc;
    }

    protected virtual void OnMouseUp()
    {
        HighlightUI.Instance.HightlightUIEnable(this);
    }

    //Queue Play시 실행될 QueueObject의 동작
    public abstract IEnumerator PlayOneTurnAction();

    public abstract void ResetFunc();
}
