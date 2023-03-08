using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_GameObjectsNodes;
    [SerializeField] Graph m_Graph;

    public void CreateGraph()
    {
        List<Node> nodes = m_Graph.CreateRandomGraphFromObjects(m_GameObjectsNodes);
        Debug.Log(m_Graph.CheckGraphIntegrity());

        foreach(Node node in nodes)
        {
            LinkViewer linkViewer = node.gameObject.AddComponent<LinkViewer>();
            linkViewer.SetNode(node);
        }
    }

    public void DestroyGraph()
    {

        foreach (GameObject gameObject in m_GameObjectsNodes)
        {
            LinkViewer linkViewer = gameObject.GetComponent<LinkViewer>();
            if (linkViewer != null)
                Destroy(linkViewer);
        }

        m_Graph.DestroyGraph();
    }
}
