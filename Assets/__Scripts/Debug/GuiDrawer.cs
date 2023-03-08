using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Debugger))]
public class GuiDrawer : MonoBehaviour
{
    [Header("Debugger layout")]
    [SerializeField] private Color backgroundColor = Color.white;
    [SerializeField] private Color fontColor = Color.black;

    private Debugger debugger;
    private readonly List<GuiContainer> containers = new();

    private void Awake()
    {
        debugger = GetComponent<Debugger>();
        WarnUserAboutBadColors(); 
        GUI.backgroundColor = backgroundColor;
        GUI.contentColor = fontColor;
    }

    private void OnGUI()
    {
        if (debugger.IsEnabled)
        {
            int x = 5, y = 5;
            int maxX = 0;

            foreach (GuiContainer container in containers)
            {
                // Check if the containers are overflowing off the screen.
                if (container.Height + y + 5 >= Screen.height)
                {
                    y = 5;
                    x += maxX + 5;
                    maxX = 0;
                }

                container.Draw(x, y);
                y += container.Height + 5;

                if (container.Width > maxX) 
                { 
                    maxX = container.Width;
                }
            }
        }
    }

    public GuiContainer CreateContainer(string _name)
    {
        GuiContainer _r = new(_name);
        containers.Add(_r);
        return _r;
    }

    public void RemoveContainer(GuiContainer container)
    {
        bool removeSucceed = containers.Remove(container);
        if (!removeSucceed) 
        {
            Debug.LogError("Error in removing container!"); 
        }
    }

    private void WarnUserAboutBadColors()
    {
        if (backgroundColor.a == 0f)
        {
            Debug.LogWarning("Debugger: Background colour is set to be invisible.");
        }
        if (fontColor.a == 0f)
        {
            Debug.LogWarning("Debugger: Font colour is set to be invisible.");
        }
    }
}
