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


    public int PlanesChangedEventFiredCount { get; private set; }

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        planeManager.planesChanged += OnPlanesChanged;
        planeManager.enabled = false;
    }

    public void StartPlaneDetection()
    {
        planeManager.enabled = true;
        PlanesChangedEventFiredCount = 0;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs planesData)
    {
        ++PlanesChangedEventFiredCount;

        int activePlanes = 0;
        foreach (ARPlane plane in planeManager.trackables)
        {
            if (plane.gameObject.activeSelf)
            {
                ++activePlanes;
            }
        }

        if (activePlanes >= PlanesToDetect)
        {
            SaveDetectedPlanes();
            planeManager.enabled = false;
            PlaneDetectionDone.Invoke();
        }
    }

    private void SaveDetectedPlanes()
    {
        PlanesDetected.Capacity = planeManager.trackables.count;
        
        if (PlanesDetected.Count != 0) 
        {
            Debug.LogWarning(name + " : you are adding planes in a list that is not empty");
        }

        int activePlanes = 0;
        foreach (ARPlane plane in planeManager.trackables)
        {
            if (activePlanes == PlanesToDetect)
            {
                plane.gameObject.SetActive(false);
                continue;
            }

            if (plane.gameObject.activeSelf)
            {
                ++activePlanes;
                PlanesDetected.Add(plane.gameObject);
            }
        }
    }

    public void HidePlanesVisualizer()
    {
        foreach (GameObject plane in PlanesDetected)
        {
            ARPlaneMeshVisualizer visualiser = plane.GetComponent<ARPlaneMeshVisualizer>();
            visualiser.enabled = false;
        }
    }

    public void ClearSavedPlanes()
    {
        foreach (GameObject plane in PlanesDetected)
        {
            plane.SetActive(false);
        }

        PlanesDetected.Clear();
    }

    public bool GetPlaneManagerEnabled()
    {
        return planeManager.enabled;
    }

    public int GetTrackablesCount()
    {
        return planeManager.trackables.count;
    }
}
