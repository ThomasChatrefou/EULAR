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

    [Button]
    public void CreateGraph()
    {
        DestroyGraph();

        List<Node> nodes = m_Graph.CreateRandomGraphFromObjects(m_Nodes, m_ColVal.colors.Count-1, iLinkScattering: m_LinkScattering);
        Debug.Assert(m_Graph.CheckGraphIntegrity());
    }

    public void DestroyGraph()
    {
        m_Graph.DestroyGraph();
    }

    public ColorToValueBindings GetColorsBinding()
    {
        return m_ColVal;
    }
}
