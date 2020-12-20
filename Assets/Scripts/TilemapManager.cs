using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    #region Singleton
    private static TilemapManager instance = null;
    public static TilemapManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<TilemapManager>();

            return instance;
        }
    }
    #endregion

    [SerializeField] private Tilemap peaks;
    [SerializeField] private Tilemap walls;
    [SerializeField] private Tilemap chests;

    public bool IsOnPeak(Vector3 pos)
    {
        Vector3Int cellPos = peaks.WorldToCell(pos);
        cellPos.z = 0;

        return peaks.HasTile(cellPos);
    }

    public bool IsOnWall(Vector3 pos, Vector3 dir)
    {
        Vector3Int cellPos = walls.WorldToCell(pos + dir);
        cellPos.z = 0;

        return walls.HasTile(cellPos);
    }

    public bool IsOnChest(Vector3 pos)
    {
        Vector3Int cellPos = chests.WorldToCell(pos);
        cellPos.z = 0;

        bool check = chests.HasTile(cellPos);

        if (check) chests.GetInstantiatedObject(cellPos).SetActive(false);

        return check;
    }
}
