using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class ARPlaneManagerViewer : DebugViewerComponent
{
    private ARPlaneManager planeManager;

    private int numberOfPlanes = 0;
    
    public enum DebugVariables
    {
        NumberOfPlanes,
    }

    protected override void AddDebugVariablesToContainer()
    {
        container.Add((int)DebugVariables.NumberOfPlanes, "Number of planes ");
    }

    protected override void UpdateDebugVariablesValue()
    {
        container.UpdateVal((int)DebugVariables.NumberOfPlanes, numberOfPlanes);
    }

    private new void Awake()
    {
        base.Awake();
        planeManager = GetComponent<ARPlaneManager>();
        planeManager.planesChanged += OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs planesData)
    {
        numberOfPlanes += planesData.added.Count;
        numberOfPlanes -= planesData.removed.Count;
    }
}
