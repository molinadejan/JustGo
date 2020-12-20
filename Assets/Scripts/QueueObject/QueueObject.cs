using System.Collections;
using UnityEngine;

public abstract class QueueObject : MonoBehaviour
{
    public delegate void ResetDele();

    private static ResetDele resetDele;
    public static void InvokeResetDele() => resetDele.Invoke();

    protected bool isOver;
    public bool IsOver => isOver;

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

    protected virtual void OnMouseUp()
    {
        HighlightUI.Instance.HightlightUIEnable(this);
    }

    public abstract IEnumerator PlayOneTurnAction();
    public abstract void ResetFunc();
}
