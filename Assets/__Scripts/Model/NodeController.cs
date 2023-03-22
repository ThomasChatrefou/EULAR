using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Node))]
public class NodeController : MonoBehaviour
{
    [SerializeField] Button m_Button;

    private Node m_Node;
    private Graph m_NodeGraph;

    private bool m_CanClick = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Button.onClick.AddListener(OnClick);
        m_Node = GetComponent<Node>();
        m_Node.OnNodeInit.AddListener(OnNodeInit);
    }

    void OnClick()
    {
        if(!m_CanClick)
            return;

        m_Node.NextValueOnNeighbours();
    }

    void OnNodeInit()
    {
        m_CanClick = true;
        m_NodeGraph = m_Node.GetGraph();
        m_NodeGraph.OnGraphCompletion.AddListener(DisableClick);
        m_NodeGraph.OnPendingGraphDesctruction.AddListener(RemoveGraphListeners);
    }

    void DisableClick()
    {
        m_CanClick = false;
    }
    void DisableClick(int i)
    {
        m_CanClick = false;
    }

    void RemoveGraphListeners()
    {
        DisableClick();
        m_NodeGraph.OnGraphCompletion.RemoveListener(DisableClick);
        m_NodeGraph.OnPendingGraphDesctruction.RemoveListener(RemoveGraphListeners);
    }
}
