using UnityEditor;
using UnityEngine;

public class DebuggerBuilderEditor : Editor
{
    [MenuItem("GameObject/Tools/Debugger")]
    public static void Init()
    {
        GameObject go = new("Debugger");
        go.AddComponent<Debugger>();
        go.AddComponent<GuiDrawer>();
        go.AddComponent<DebuggerInterface>();
    }
}
