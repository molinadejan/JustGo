using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class QueueObject : MonoBehaviour
{
    public delegate void ResetDele();

    private static ResetDele resetDele;
    public static void InvokeResetDele() => resetDele.Invoke();

    protected bool isOver;
    public bool IsOver => isOver;

    [SerializeField] private UnityEvent clickedEvent;
    public void InvokeClickedEvent() => clickedEvent.Invoke();

    protected Animator animator;
    protected BoxCollider2D col;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    public virtual void Start()
    {
        resetDele += ResetFunc;
    }

    private void OnMouseUp()
    {
        clickedEvent.Invoke();
    }

    // Queue에서 실행될 함수
    public abstract IEnumerator PlayOneTurnAction();
    public abstract void ResetFunc();
}
