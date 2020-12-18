using System.Collections;
using UnityEngine;

public abstract class QueueObject : MonoBehaviour
{
    public delegate void ResetDele();
    public static ResetDele resetDele;

    protected bool isOver;
    public bool IsOver => isOver;

    // Queue에서 실행될 함수
    public abstract IEnumerator PlayOneTurnAction();
    public abstract void ResetFunc();

    public virtual void Start()
    {
        resetDele += ResetFunc;
    }
}
