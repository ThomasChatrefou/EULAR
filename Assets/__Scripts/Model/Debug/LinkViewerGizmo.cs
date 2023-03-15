using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkViewerGizmo : MonoBehaviour
{
    private Node m_Node;
    private HashSet<Node> m_Neighbours;

    public void SetNode(Node iNode)
    {
        m_Node = iNode;
    }

    private void OnDrawGizmos()
    {
        if(m_Node == null)
            return;

        if(m_Neighbours == null)
            m_Neighbours = m_Node.GetNeighbours();

        Gizmos.color = Color.white;

        foreach(Node neighbour in m_Neighbours)
            Gizmos.DrawLine(transform.position, neighbour.transform.position);
    }
}
