using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARPlaneManager))]
public class PlaneController : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;

    public int PlanesToDetect { get; set; }

    public List<GameObject> PlanesDetected { get; private set; } = new();
    public event Action PlaneDetectionDone;

    private int trackablesStartIndex = 0;

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        planeManager.planesChanged += OnPlanesChanged;
        planeManager.enabled = false;
    }

    public void StartPlaneDetection()
    {
        planeManager.enabled = true;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs planesData)
    {
        if (planeManager.trackables.count == PlanesToDetect)
        {
            SaveDetectedPlanes();
            planeManager.enabled = false;
            PlaneDetectionDone.Invoke();
        }
    }

    private void SaveDetectedPlanes()
    {
        PlanesDetected.Clear();
        PlanesDetected.Capacity = planeManager.trackables.count;
        
        if (PlanesDetected.Count != 0) 
        {
            Debug.LogWarning(name + " : you are adding planes in a list that is not empty");
        }

        TrackableCollection<ARPlane>.Enumerator planesEnumerator = planeManager.trackables.GetEnumerator();
        for (int i = 0; i < trackablesStartIndex; i++)
        {
            planesEnumerator.MoveNext();
        }

        for (int i = 0; i < PlanesToDetect; i++)
        {
            ARPlane plane = planesEnumerator.Current;
            PlanesDetected.Add(plane.gameObject);
            planesEnumerator.MoveNext();
        }
        trackablesStartIndex += PlanesToDetect;
    }

    public void HidePlanesVisualizer()
    {
        foreach (GameObject plane in PlanesDetected)
        {
            ARPlaneMeshVisualizer visualiser = plane.GetComponent<ARPlaneMeshVisualizer>();
            visualiser.enabled = false;
        }
    }
}
