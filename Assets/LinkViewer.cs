using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkViewer : MonoBehaviour
{
    private Node m_Node;

    public void SetNode(Node iNode)
    {
        m_Node = iNode;
    }

    private void OnDrawGizmos()
    {
        if(m_Node == null)
        {
            Debug.Log("not drawing");
            return;
        }

        Debug.Log("drawing");
        Gizmos.color = Color.white;

        foreach(Node neighbour in m_Node.GetNeighbours())
            Gizmos.DrawLine(transform.position, neighbour.transform.position);
    }
}
