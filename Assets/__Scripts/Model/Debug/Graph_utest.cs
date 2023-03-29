using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Graph_utest : MonoBehaviour
{
    [SerializeField] GameObject m_EmptyGameObjectPrefab;

    private bool m_IsValid;
    private int m_Count;

    // Start is called before the first frame update
    void Start()
    {
        Graph graph = gameObject.AddComponent<Graph>();
        List<Node> nodes = new List<Node>();

        ///////////////////////////////////////////////////
        // Test 0
        Assert.IsFalse(graph.CheckGraphIntegrity(false));
        Assert.AreEqual(-1, graph.GetMaxNodeValue());

        ///////////////////////////////////////////////////
        // Test 1
        nodes.Add(gameObject.AddComponent<Node>());
        nodes.Add(gameObject.AddComponent<Node>());
        m_IsValid = false;
        graph.OnInitGraph.AddListener(Validate);
        graph.CreateRandomGraphFromObjects(nodes);
        graph.OnInitGraph.RemoveListener(Validate);
        Assert.IsTrue(m_IsValid);

        Assert.AreEqual(3, graph.GetMaxNodeValue());

        // testing resolution
        m_Count = 0;
        m_IsValid = false;
        graph.OnGraphCompletion.AddListener(Validate);
        nodes[1].OnValueChanged.AddListener(Incr);
        for(int i = 0; i <= graph.GetMaxNodeValue(); i++)
            nodes[0].NextValueOnNeighbours();
        graph.OnGraphCompletion.RemoveListener(Validate);
        nodes[1].OnValueChanged.RemoveListener(Incr);
        Assert.IsTrue(m_IsValid);
        Assert.AreEqual(graph.GetMaxNodeValue(), m_Count-1);

        m_IsValid = false;
        graph.OnPendingGraphDesctruction.AddListener(Validate);
        graph.DestroyGraph();
        graph.OnPendingGraphDesctruction.RemoveListener(Validate);
        Assert.IsTrue(m_IsValid);

        _DestroyNodes(nodes);

        ///////////////////////////////////////////////////
        // Test 2
        for(int ii = 0; ii < 16; ii++)
            nodes.Add(gameObject.AddComponent<Node>());

        int nIter = 50;
        for(int i = 0; i < nIter; i++)
        {
            int maxValue = i / 10 + 3;
            graph.CreateRandomGraphFromObjects(nodes, iMaxValue:maxValue, iLinkScattering:(i % 10 + 2));

            Assert.IsTrue(graph.CheckGraphIntegrity());
            Assert.AreEqual(0, graph.GetNbMove());
            Assert.AreEqual(maxValue, graph.GetMaxNodeValue());

            graph.DestroyGraph();
        }

        _DestroyNodes(nodes);
        Destroy(gameObject);
    }

    void Validate()
    {
        m_IsValid = true;
    }
    void Validate(int i)
    {
        m_IsValid = true;
    }
    void Incr(int i)
    {
        m_Count++;
    }

    private void _DestroyNodes(List<Node> iNodes)
    {
        foreach(Node node in iNodes)
            Destroy(node);

        iNodes.Clear();
    }
}
