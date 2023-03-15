using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorToValueBindings", menuName = "Graph/New color bindings")]
public class ColorToValueBindings : ScriptableObject
{
    public List<Color> colors = new List<Color>();
}
