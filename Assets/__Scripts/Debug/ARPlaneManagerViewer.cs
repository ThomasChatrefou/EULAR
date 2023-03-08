using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlaneManagerViewer : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private float debugRefreshRate = 0.2f;

    private DebuggerInterface debugger;
    private GuiContainer container;

    private int numberOfPlanes = 0;

    private enum DebugVariables
    {
        NumberOfPlanes,
    }

    private void Awake()
    {
        debugger = DebuggerInterface.Instance;
        if (debugger == null)
        {
            enabled = false;
            return;
        }

        if (planeManager != null)
        {
            planeManager.planesChanged += OnPlanesChanged;
        }
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs planesData)
    {
        numberOfPlanes += planesData.added.Count;
        numberOfPlanes -= planesData.removed.Count;
    }

    private void OnEnable()
    {
        container = debugger.CreateContainer(name);
        container.Add((int)DebugVariables.NumberOfPlanes, "Number of planes ");

        debugger.OnDebuggerEnabled += StartUpdateDebugValues;
        debugger.OnDebuggerDisabled += StopUpdateDebugValues;

        if (debugger.IsEnabled)
        {
            StartUpdateDebugValues();
        }
    }

    private void OnDisable()
    {
        if (debugger != null)
        {
            StopUpdateDebugValues();

            debugger.OnDebuggerEnabled -= StartUpdateDebugValues;
            debugger.OnDebuggerDisabled -= StopUpdateDebugValues;

            debugger.RemoveContainer(container);
        }
    }

    private void StartUpdateDebugValues()
    {
        StartCoroutine(UpdateDebugValues());
    }

    private void StopUpdateDebugValues()
    {
        StopAllCoroutines();
    }

    private IEnumerator UpdateDebugValues()
    {
        while (debugger.IsEnabled)
        {
            print(name);
            container.UpdateVal((int)DebugVariables.NumberOfPlanes, numberOfPlanes);
            yield return new WaitForSeconds(debugRefreshRate);
        }
    }
}
