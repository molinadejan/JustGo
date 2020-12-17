using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Priest : MonoBehaviour
{
    public float moveAmount;
    public float moveTime;
    public Ease ease;

    [SerializeField] private List<Vector2> dirList;

    public List<Vector2> DirList { get { return dirList; } }

    private void Awake()
    {
        dirList = new List<Vector2>();
    }

    private void Move(Vector3 dir)
    {
        transform.DOMove(transform.position + dir * moveAmount, moveTime).SetEase(ease);
    }

    private void OnMouseUp()
    {
        UIManager.Instance.ArrowUIEnable(this);
    }
}
