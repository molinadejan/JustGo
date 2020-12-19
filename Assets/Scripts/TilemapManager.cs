using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    #region Singleton
    private static TilemapManager instance = null;
    public static TilemapManager Instance => instance;
    #endregion

    [SerializeField] private Tilemap peaks;
    [SerializeField] private Tilemap walls;
    
    private void Awake()
    {
        if (instance == null) instance = this;
    }

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
}
