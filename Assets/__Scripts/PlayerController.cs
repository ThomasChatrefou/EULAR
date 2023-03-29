using NaughtyAttributes;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GraphManager graphManager;
    [SerializeField] private PlaneController planeController;

    private void Start()
    {
        if (planeController != null)
        {
            Debug.LogError(name + " : plane controller ref is missing!");
        }
    }

    [Button]
    public void OnDetectButtonPressed()
    {
        planeController.StartPlaneDetection();
    }

    // this should be available only if detection is finished
    [Button]
    public void OnGeneratePuzzlePressed()
    {
        planeController.HidePlanesVisualizer();
        graphManager.CreateGraph();
    }

    [Button]
    public void OnResetPuzzlePressed()
    {
        graphManager.DestroyGraph();
        graphManager.DestroyNodes();
        planeController.ClearSavedPlanes();
    }
}
