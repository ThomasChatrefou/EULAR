using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneToGraphLinker : MonoBehaviour
{
    [SerializeField] private GraphManager graphManager;
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private PlaneController planeController;
    [SerializeField] private Camera arCamera;

    private void Awake()
    {
        if (graphManager == null)
        {
            Debug.LogError("missing ref for graph manager");
            return;
        }
        if (planeManager == null)
        {
            Debug.LogError("missing ref for plane manager");
            return;
        }

        planeManager.planesChanged += OnPlanesChanged;
        planeController.PlaneDetectionDone += OnPlaneDetectionDone;
        arCamera = Camera.main;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs planesData)
    {
        List<ARPlane> planeList = planesData.added;
        foreach (ARPlane plane in planeList)
        {
            NodeViewer nodeView = plane.gameObject.GetComponent<NodeViewer>();
            nodeView.GraphManager = graphManager;
        }
    }

    private void OnPlaneDetectionDone()
    {
        List<Node> nodes = new()
        {
            Capacity = planeController.PlanesDetected.Count
        };

        foreach (GameObject plane in planeController.PlanesDetected)
        {
            Node node = plane.GetComponentInChildren<Node>();
            nodes.Add(node);

            NodeViewer nodeView = plane.GetComponentInChildren<NodeViewer>();
            nodeView.GraphManager = graphManager;

            LookAtCameraBehaviour lookAtMe = plane.GetComponentInChildren<LookAtCameraBehaviour>();
            lookAtMe.MainCamera = arCamera.transform;

            Canvas nodeCanvas = plane.GetComponentInChildren<Canvas>();
            nodeCanvas.worldCamera = arCamera;
        }

        //graphManager.CreateGraphFromNodes(nodes);
        graphManager.Nodes = nodes; // graph creation can be fired later
    }
}
