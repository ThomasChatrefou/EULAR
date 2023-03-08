using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

using Rand = UnityEngine.Random;

public class Graph : MonoBehaviour
{
    public UnityEvent OnInitGraph;

    // value of nodes shall be within 0 and m_MaxValue
    // negative value means unitialized
    private int m_MaxValue = -1;

    private List<Node> m_Nodes = new List<Node>();

    // Start is called before the first frame update
    void Awake()
    {
        if(OnInitGraph == null)
            OnInitGraph = new UnityEvent();
    }

    public int GetMaxNodeValue()
    {
        return m_MaxValue;
    }

    // creating nodes from gameobjects
    // with random links
    // return one node of the graph
    public List<Node> CreateRandomGraphFromObjects(GameObject[] iPoints, int iMaxValue = 3)
    {
        m_MaxValue = iMaxValue;

        // node init
        foreach(GameObject point in iPoints)
        {
            Node pointNode = point.AddComponent<Node>();
            pointNode.Init(this);
            m_Nodes.Add(pointNode);
        }

        // creating random links
        int currentNodeIdx = 0;
        foreach(Node node in m_Nodes)
        {
            // choosing a random number of link between 1 and maxLinkAvailable
            // we don't want to many links, so we twist the distribution a bit
            int nbAvailableLinks = m_Nodes.Count - 1;
            int nbNoLinkSq = Rand.Range(1, nbAvailableLinks * nbAvailableLinks);
            int nbNoLink = (int)Math.Ceiling(Math.Sqrt(nbNoLinkSq)); // this is the number of nodes that won't get linked to the current node
            int nbLink = nbAvailableLinks - nbNoLink;

            // creating links by random sampling
            int[] nodeToLinkIndices = _RandomSampleNeighbours(currentNodeIdx, nbLink);
            foreach(int nodeIndex in nodeToLinkIndices)
                Node.AddLink(m_Nodes[currentNodeIdx], m_Nodes[nodeIndex]);

            currentNodeIdx++;
        }

        // scramble solution
        foreach(Node node in m_Nodes)
        {
            int nAction = Rand.Range(0, m_MaxValue);
            for (int i = 0; i < nAction; i++)
                node.NextValueOnNeighbours();
        }

        OnInitGraph.Invoke();

        return m_Nodes;
    }

    private int[] _RandomSampleNeighbours(int iNodeIdx, int iNbNeighbours)
    {
        int[] nodeToLinkIndices = new int[iNbNeighbours];
        for(int iLink = 0; iLink < iNbNeighbours; iLink++)
            nodeToLinkIndices[iLink] = Rand.Range(0, (m_Nodes.Count - 1) - iLink);

        for(int nodeToLinkIndex_Index = 0; nodeToLinkIndex_Index < iNbNeighbours; nodeToLinkIndex_Index++)
        {
            for(int nodeToLinkReducedIndex_Index = nodeToLinkIndex_Index + 1; nodeToLinkReducedIndex_Index < iNbNeighbours; nodeToLinkReducedIndex_Index++)
            {
                if(nodeToLinkIndices[nodeToLinkReducedIndex_Index] >= nodeToLinkIndices[nodeToLinkIndex_Index])
                    nodeToLinkIndices[nodeToLinkReducedIndex_Index]++;
            }

            if(nodeToLinkIndices[nodeToLinkIndex_Index] >= iNodeIdx)
                nodeToLinkIndices[nodeToLinkIndex_Index]++;
        }

        return nodeToLinkIndices;
    }

    // we ensure that:
    // * there is no self linked node 
    // * all links are non-directionnal
    public bool CheckGraphIntegrity()
    {
        foreach(Node node in m_Nodes)
        {
            if (!node.CheckNodeIntegrity())
                return false;
        }

        // #TODO need to check that graph is in one piece

        return true;
    }

    public void DestroyGraph()
    {
        foreach (Node node in m_Nodes)
            Destroy(node);

        m_MaxValue = -1;
    }
}
