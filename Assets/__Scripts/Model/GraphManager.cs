using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    [SerializeField] List<Node> m_Nodes = new List<Node>();
    [SerializeField] Graph m_Graph;
    [SerializeField] float m_LinkScattering = 2;
    [SerializeField] ColorToValueBindings m_ColVal;

    public List<Node> Nodes 
    { 
        get { return m_Nodes; }
        set { m_Nodes = value; }
    }

    [Button]
    public void CreateGraph()
    {
        DestroyGraph();
        CreateGraphFromNodes(m_Nodes);
        Debug.Assert(m_Graph.CheckGraphIntegrity());
    }

    public void DestroyGraph()
    {
        m_Graph.DestroyGraph();
    }

    public void DestroyNodes()
    {
        foreach(Node node in m_Nodes)
        {
            Destroy(node.gameObject);
        }
    }

    public ColorToValueBindings GetColorsBinding()
    {
        return m_ColVal;
    }

    public void CreateGraphFromNodes(List<Node> nodes)
    {
        m_Graph.CreateRandomGraphFromObjects(nodes, m_ColVal.colors.Count - 1, iLinkScattering: m_LinkScattering);
    }
}
