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

        isOver = false;
    }

    public void RemoveAtDirList(int index)
    {
        dirList.RemoveAt(index);
        isOver = dirList.Count == 0 ? true : false;
    }

    /// <summary>
    /// PlayQueue() 호출 시 다음 Vector3값을 리턴합니다.
    /// </summary>
    /// <returns>다음 이동할 방향</returns>
    private Vector3 GetNextDir()
    {
        Vector3 ret = dirList[listIndex++];

        if (listIndex >= dirList.Count) isOver = true;

        return ret;
    }

    private int listIndex;
}
