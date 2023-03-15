using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Node))]
public class NodeViewer : MonoBehaviour
{
    [SerializeField] GraphManager m_GraphManager;

    private Renderer m_Renderer;
    private Node m_Node;

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_Node = GetComponent<Node>();
        m_Node.OnValueChanged.AddListener(OnValueChanged);
    }

    // Update is called once per frame
    void OnValueChanged(int iOldVal)
    {
        int val = m_Node.GetValue();
        if(val < 0)
        {
            m_Renderer.material.color = Color.white;
            return;
        }

        m_Renderer.material.color = m_GraphManager.GetColorsBinding().colors[val];
    }
}
