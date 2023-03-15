using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph_utest : MonoBehaviour
{
    [SerializeField] GameObject m_EmptyGameObjectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Graph graph = gameObject.AddComponent<Graph>();
        List<Node> nodes = new List<Node>();

        ///////////////////////////////////////////////////
        // Test 0
        Debug.Assert(!graph.CheckGraphIntegrity(false));

        ///////////////////////////////////////////////////
        // Test 1
        nodes.Add(gameObject.AddComponent<Node>());
        nodes.Add(gameObject.AddComponent<Node>());
        graph.CreateRandomGraphFromObjects(nodes);

        graph.DestroyGraph();
        _DestroyNodes(nodes);

        ///////////////////////////////////////////////////
        // Test 2
        for(int ii = 0; ii < 16; ii++)
            nodes.Add(gameObject.AddComponent<Node>());
        
        for(int i = 0; i < 50; i++)
        {
            graph.CreateRandomGraphFromObjects(nodes, iLinkScattering:10);

            Debug.Assert(graph.CheckGraphIntegrity());

            graph.DestroyGraph();
        }

        _DestroyNodes(nodes);
        Destroy(gameObject);
    }

    private void _DestroyNodes(List<Node> iNodes)
    {
        foreach(Node node in iNodes)
            Destroy(node);

        iNodes.Clear();
    }
}
