using System.Collections;
using GameObjects;
using UnityEngine;
using Enums;
using Extensions;

public class ScannerButton : MonoBehaviour
{
    public Room ReferencedRoom;

    public GameObject ScannerIcon;

    [Header("Detection tags")]
    public GameObject NothingDetected;
    public GameObject SomethingDetected;
    public GameObject DefinitelyDetected;

    private void OnEnable()
    {
        ScannerIcon.SetActive(true);

        NothingDetected.SetActive(false);
        SomethingDetected.SetActive(false);
        DefinitelyDetected.SetActive(false);
    }

    public void ScanRoom()
    {
        GameObject currentlyActiveTag = null;
        var scanResult = ReferencedRoom.Scanner.Scan();

        switch (scanResult)
        {
            case ScanResult.NothingDetected:
                currentlyActiveTag = NothingDetected;
                break;
            case ScanResult.SomethingDetected:
                currentlyActiveTag = SomethingDetected;
                break;
            case ScanResult.DefinitelyDetected:
                currentlyActiveTag = DefinitelyDetected;
                break;
        }

        ScannerIcon.SetActive(false);
        currentlyActiveTag?.SetActive(true);

        StartCoroutine(ScannerIcon.SetActiveAfterDelay(true, 3f));
        StartCoroutine(currentlyActiveTag.SetActiveAfterDelay(false, 3f));
    }
}
