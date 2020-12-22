using System.Collections.Generic;
using UnityEngine;

public partial class MovingObject
{
    protected List<Vector3> dirList = new List<Vector3>();
    public List<Vector3> DirList => dirList;

    protected int listIndex;

    public virtual void AddDirList(string dir)
    {
        if (dir.Equals("up")) dirList.Add(Vector3.up);
        else if (dir.Equals("down")) dirList.Add(Vector3.down);
        else if (dir.Equals("left")) dirList.Add(Vector3.left);
        else dirList.Add(Vector3.right);
    }

    public abstract Vector3 GetNextDir();
}
