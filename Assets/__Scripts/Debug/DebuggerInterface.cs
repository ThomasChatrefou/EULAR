using System;
using UnityEngine;

[RequireComponent(typeof(Debugger))]
[RequireComponent(typeof(GuiDrawer))]
public class DebuggerInterface : MonoBehaviour
{
    public static DebuggerInterface Instance { get; private set; }

    public bool IsEnabled => debugger.IsEnabled;

    public event Action OnDebuggerEnabled;
    public event Action OnDebuggerDisabled;

    private Debugger debugger;
    private GuiDrawer drawer;

    private void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Instance = this;
        }

        debugger = GetComponent<Debugger>();
        drawer = GetComponent<GuiDrawer>();

        debugger.OnToggle += OnDebuggerToggle;
    }

    private void OnDebuggerToggle()
    {
        if (IsEnabled)
        {
            OnDebuggerEnabled.Invoke();
        }
        else
        {
            OnDebuggerDisabled.Invoke();
        }
    }

    public GuiContainer CreateContainer(string containerName)
    {
        return drawer.CreateContainer(containerName);
    }

    public void RemoveContainer(GuiContainer container)
    {
        drawer.RemoveContainer(container);
    }
}