using System.Collections;

public class Skeleton : Enemy
{
    public override IEnumerator CheckPeakCor()
    {
        // Peak에 죽음
        if (TilemapManager.Instance.IsOnPeak(transform.position))
        {
            isOver = true;
            animator.Play("Die");
        }

        yield return null;
    }
}
