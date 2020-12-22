using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up,
    down,
    left,
    right
}

public partial class Enemy
{
    [SerializeField] private List<Direction> directionList;

    // Enemy가 이동할 리스트
    private List<Vector3> dirList = new List<Vector3>();
    public List<Vector3> DirList => dirList;

    private void AddDirList(string dir)
    {
        if (dir.Equals("up")) dirList.Add(Vector3.up);
        else if (dir.Equals("down")) dirList.Add(Vector3.down);
        else if (dir.Equals("left")) dirList.Add(Vector3.left);
        else if (dir.Equals("right")) dirList.Add(Vector3.right);
        else return;
    }

    private Vector3 GetNextDir()
    {
        Vector3 ret = dirList[listIndex++];
        if (listIndex >= dirList.Count) listIndex = 0;
        return ret;
    }

    private int listIndex;
}
