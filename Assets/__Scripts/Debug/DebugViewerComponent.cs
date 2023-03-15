using System.Collections;
using UnityEngine;

public class DebugViewerComponent : MonoBehaviour
{
    [SerializeField] private float debugRefreshRate = 0.2f;

    protected DebuggerInterface debugger;
    protected GuiContainer container;

    protected virtual void AddDebugVariablesToContainer() { }
    protected virtual void UpdateDebugVariablesValue() { }

    public virtual string ContainerName
    {
        get { return name; }
    }

    protected void Awake()
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
        container = debugger.CreateContainer(ContainerName);
        AddDebugVariablesToContainer();
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
            UpdateDebugVariablesValue();
            yield return new WaitForSeconds(debugRefreshRate);
        }
    }
}
