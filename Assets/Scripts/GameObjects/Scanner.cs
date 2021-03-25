using Classes.Abstracts;
using Enums;
using GameObjects;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public Room Room;

    [Header("Scanner settings")]
    [Range(0, 1)]
    public float ScannerCertainty = 0.2f;

    public ScanResult Scan()
    {
        var rn = Random.Range(0f, 1f);

        if (rn <= ScannerCertainty)
        {
            return ScanResult.SomethingDetected;
        }
        else if (Room.EnemiesInRoom.Count > 0)
        {
            return ScanResult.DefinitelyDetected;
        }
        else
        {
            return ScanResult.NothingDetected;
        }
    }
}
