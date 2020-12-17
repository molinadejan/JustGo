using DG.Tweening;
using UnityEngine;

public class Priset : MonoBehaviour
{
    public float moveAmount;
    public float moveTime;
    public Ease ease;

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) Move();
    }
    */

    private void Move()
    {
        transform.DOMove(transform.position + Vector3.up * moveAmount, moveTime).SetEase(ease);
    }
}
