using UnityEngine;

// Preist의 DirList를 관리하는 Preist의 partial class
public partial class Priest
{
    // Priest의 DirList에 새로운 Vector3를 추가합니다.
    public override void AddDirList(string dir)
    {
        base.AddDirList(dir);
        isOver = false;
    }

    public void RemoveAtDirList(int index)
    {
        dirList.RemoveAt(index);
        isOver = dirList.Count == 0 ? true : false;
    }

    public override Vector3 GetNextDir()
    {
        Vector3 ret = dirList[listIndex++];
        if (listIndex >= dirList.Count) isOver = true;
        return ret;
    }
}
