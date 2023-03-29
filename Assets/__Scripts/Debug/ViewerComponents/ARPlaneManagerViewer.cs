using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class ARPlaneManagerViewer : DebugViewerComponent
{
    private PlaneController planeController;

    public enum DebugVariables
    {
        NumberOfPlanes,
        PlanesToDetect,
        PlanesDetectionEnabled,
        PlanesChangedEventFiredCount
    }

    protected override void AddDebugVariablesToContainer()
    {
        container.Add((int)DebugVariables.NumberOfPlanes, "Number of planes          ");
        container.Add((int)DebugVariables.PlanesToDetect, "nb of planes to detect    ");
        container.Add((int)DebugVariables.PlanesDetectionEnabled, "planes detection enabled ");
        container.Add((int)DebugVariables.PlanesChangedEventFiredCount, "nb planes changed event fired ");
    }

    protected override void UpdateDebugVariablesValue()
    {
        container.UpdateVal((int)DebugVariables.NumberOfPlanes, planeController.GetTrackablesCount());
        container.UpdateVal((int)DebugVariables.PlanesToDetect, planeController.PlanesToDetect);
        container.UpdateVal((int)DebugVariables.PlanesDetectionEnabled, planeController.GetPlaneManagerEnabled());
        container.UpdateVal((int)DebugVariables.PlanesChangedEventFiredCount, planeController.PlanesChangedEventFiredCount);
    }

    private new void Awake()
    {
        base.Awake();
        planeController = GetComponent<PlaneController>();
    }
}
