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

    public override Vector3 GetNextDir()
    {
        Vector3 ret = dirList[listIndex++];
        if (listIndex >= dirList.Count) listIndex = 0;
        return ret;
    }
}
