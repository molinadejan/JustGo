using System.Collections.Generic;
using UnityEngine;

// Preist의 DirList를 관리하는 Preist의 partial class

public partial class Priest
{
    [SerializeField] private List<Vector3> dirList = new List<Vector3>();
    public List<Vector3> DirList => dirList;

    /// <summary>
    /// Priest의 DirList에 새로운 Vector3를 추가합니다.
    /// </summary>
    /// <param name="dir">새로 추가할 방향</param>
    public void AddDirList(string dir)
    {
        if (dir.Equals("up")) dirList.Add(Vector3.up);
        else if (dir.Equals("down")) dirList.Add(Vector3.down);
        else if (dir.Equals("left")) dirList.Add(Vector3.left);
        else if (dir.Equals("right")) dirList.Add(Vector3.right);
        else return;

        // DirList가 비어있지 않다면 isOver를 false로 합니다.
        isOver = false;
    }

    public void RemoveAtDirList(int index)
    {
        dirList.RemoveAt(index);
        isOver = dirList.Count == 0 ? true : false;
        //DirList가 비어있다면 isOver를 true로 합니다.
    }

    /// <summary>
    /// PlayQueue() 호출 시 다음 Vector3값을 리턴합니다.
    /// </summary>
    /// <returns>다음 이동할 방향</returns>
    private Vector3 GetNextDir()
    {
        Vector3 ret = dirList[listIndex++];

        // DirList를 모두 이동했으면 isOver를 true로 합니다.
        if (listIndex >= dirList.Count) isOver = true;

        return ret;
    }

    private int listIndex;
}
