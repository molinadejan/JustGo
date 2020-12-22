public abstract partial class Enemy : MovingObject
{
    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < directionList.Count; i++)
            AddDirList(directionList[i].ToString());
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        ArrowUI.Instance.ArrowUIEnable(this);
    }
}
