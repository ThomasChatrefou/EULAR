using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Debugger : MonoBehaviour
{
    public bool IsEnabled { get; private set; }
    public event Action OnToggle;

    private DebuggerInputs inputs;

    private void Awake()
    {
        inputs = new DebuggerInputs();
        inputs.Debug.Toggle.performed += ToggleDebugger;
    }

    private void ToggleDebugger(InputAction.CallbackContext obj)
    {
        IsEnabled = !IsEnabled;
        string state = IsEnabled ? "enabled" : "disabled";
        print("Debug mode " + state);
        
        OnToggle.Invoke();
    }

    private void OnEnable()
    {
        inputs.Debug.Enable();
    }

    private void OnDisable()
    {
        inputs.Debug.Disable();
    }
}
