using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Priest : MonoBehaviour
{
    public float moveAmount;
    public float moveTime;
    public Ease ease;

    [SerializeField] private List<Vector3> dirList;

    public List<Vector3> DirList { get { return dirList; } }

    private void Awake()
    {
        dirList = new List<Vector3>();
    }

    private void Move(Vector3 dir)
    {
        transform.DOMove(transform.position + dir * moveAmount, moveTime).SetEase(ease);
    }
}
