using System.Collections;

public class Skull : Enemy
{
    public override IEnumerator CheckPeakCor()
    {
        // Skull은 가시에 안찔립니다
        yield return null;
    }
}
