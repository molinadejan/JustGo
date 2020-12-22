using System.Collections;

public class Vampire : Enemy
{
    public override IEnumerator CheckPeakCor()
    {
        // Vampire는 가시에 안찔립니다
        // Vampire는 밀리지 않습니다.
        yield return null;
    }
}
