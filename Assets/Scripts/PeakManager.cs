using UnityEngine;
using UnityEngine.Tilemaps;

public class PeakManager : MonoBehaviour
{
    #region Singleton
    private static PeakManager instance = null;
    public static PeakManager Instance => instance;
    #endregion

    [SerializeField] private Tilemap peaks;
    
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
}
