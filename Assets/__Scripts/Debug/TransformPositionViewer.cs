using System.Collections;
using UnityEngine;

public class TransformPositionViewer : MonoBehaviour
{
    [SerializeField] private float debugRefreshRate = 0.2f;

    private DebuggerInterface debugger;
    private GuiContainer container;

    private enum DebugVariables
    {
        Position,
    }

    private void Awake()
    {
        debugger = DebuggerInterface.Instance;
        if (debugger == null)
        {
            enabled = false;
            return;
        }
    }

    private void OnEnable()
    {
        container = debugger.CreateContainer(name);
        container.Add((int)DebugVariables.Position, "Position ");

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
            container.UpdateVal((int)DebugVariables.Position, transform.position);
            yield return new WaitForSeconds(debugRefreshRate);
        }
    }
}