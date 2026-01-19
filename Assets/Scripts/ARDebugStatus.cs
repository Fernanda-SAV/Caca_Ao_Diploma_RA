using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARDebugStatus : MonoBehaviour
{
    public Text textLegacy; // se estiver usando Text antigo
    public TMPro.TMP_Text textTMP; // se estiver usando TextMeshPro

    public ARSession arSession;
    public ARPlaneManager planeManager;
    public ARRaycastManager raycastManager;

    void Update()
    {
        if (arSession == null || planeManager == null)
            return;

        string msg =
            $"ARSession enabled: {arSession.enabled}\n" +
            $"ARSession state: {ARSession.state}\n" +
            $"ARSession installStatus: {ARSession.notTrackingReason}\n" +
            $"Planes detected: {planeManager.trackables.count}\n" +
            $"PlaneManager enabled: {planeManager.enabled}\n" +
            $"RaycastManager enabled: {(raycastManager != null ? raycastManager.enabled.ToString() : "null")}\n";

        if (textTMP != null) textTMP.text = msg;
        if (textLegacy != null) textLegacy.text = msg;
    }
}
