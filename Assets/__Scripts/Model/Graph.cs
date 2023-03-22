using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

using Rand = UnityEngine.Random;
using HasEndedEvent = UnityEngine.Events.UnityEvent<int>;

public class Graph : MonoBehaviour
{
    public UnityEvent OnInitGraph;
    public UnityEvent OnPendingGraphDesctruction;
    public HasEndedEvent OnGraphCompletion;

    private bool hasBeenInit = false;
    // value of nodes shall be within 0 and m_MaxValue
    // negative value means unitialized
    private int m_MaxValue = -1;
    // record the number of move until completion
    private int m_NbMove = 0;

    private List<Node> m_Nodes = new List<Node>();

    // Start is called before the first frame update
    void Awake()
    {
        if(OnInitGraph == null)
            OnInitGraph = new UnityEvent();

        if(OnPendingGraphDesctruction == null)
            OnPendingGraphDesctruction = new UnityEvent();

        if(OnGraphCompletion == null)
            OnGraphCompletion = new HasEndedEvent();
    }

    public int GetMaxNodeValue()
    {
        return m_MaxValue;
    }

    // creating random links between given nodes
    // the higher iLinkScattering is, the less links there will be in the graph
    public List<Node> CreateRandomGraphFromObjects(List<Node> iNodes, int iMaxValue = 3, float iLinkScattering = 2)
    {
        m_MaxValue = iMaxValue;

        // nodes init
        foreach(Node node in iNodes)
        {
            node.Init(this);
            m_Nodes.Add(node);
        }

        // creating random links
        int currentNodeIdx = 0;
        foreach(Node node in m_Nodes)
        {
            // choosing a random number of link between 1 and maxLinkAvailable
            // we don't want to many links, so we twist the distribution a bit
            int nbAvailableLinks = m_Nodes.Count - 1;

            // we use the distribution given by the inverse of f on the interval [0, f(1)]
            // where f(x) = 1 / Math.Pow(x, iLinkScattering) - (1 / Math.Pow(nbAvailableLinks, iLinkScattering))
            double b = 1 / Math.Pow(nbAvailableLinks, iLinkScattering);
            double upperBound = 1 - b;
            double randDouble = Rand.Range(0, (float)upperBound);
            int nbLink = Mathf.FloorToInt((float)(1 / Math.Pow(randDouble + b, 1 / iLinkScattering)));

            // creating links by random sampling
            int[] nodeToLinkIndices = _RandomSampleNeighbours(currentNodeIdx, nbLink);
            foreach(int nodeIndex in nodeToLinkIndices)
                Node.AddLink(m_Nodes[currentNodeIdx], m_Nodes[nodeIndex]);

            currentNodeIdx++;
        }

        // parse nodes to check wether the graph is in one piece
        // if not, add a new link and parse again
        HashSet<Node> visitedNodes = new HashSet<Node>();
        _VisitNode(m_Nodes[0], visitedNodes);
        while(visitedNodes.Count != m_Nodes.Count)
        {
            // finding a node that is not linked to the visited nodes
            Node nodeToLink = null;
            foreach(Node node in m_Nodes)
            {
                if(visitedNodes.Contains(node))
                    continue;

                nodeToLink = node;
                break;
            }
            Assert.IsNotNull(nodeToLink);

            int visitedNodeIdx = Rand.Range(0, visitedNodes.Count);
            HashSet<Node>.Enumerator itVisitedNode = visitedNodes.GetEnumerator();
            for(int ii = 0; ii <= visitedNodeIdx; ii++)
                itVisitedNode.MoveNext();
            Assert.IsNotNull(itVisitedNode.Current);

            Node.AddLink(nodeToLink, itVisitedNode.Current);
            _VisitNode(nodeToLink, visitedNodes);
        }

        // scramble solution
        foreach(Node node in m_Nodes)
        {
            int nAction = Rand.Range(0, iMaxValue + 1);
            for(int i = 0; i < nAction; i++)
                node.NextValueOnNeighbours();
        }

        m_NbMove = 0; // reset number of move after scrambling
        hasBeenInit = true;

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
    // * graph is not empty
    // * all nodes are valid
    // * graph is in one piece
    public bool CheckGraphIntegrity(bool iVerbose = true)
    {
        if(m_Nodes.Count == 0)
        {
            if(iVerbose)
                Debug.LogError("Graph is empty");

            return false;
        }

        foreach(Node node in m_Nodes)
        {
            if(!node.CheckNodeIntegrity(iVerbose))
                return false;
        }

        HashSet<Node> visitedNodes = new HashSet<Node>();
        _VisitNode(m_Nodes[0], visitedNodes);
        if(visitedNodes.Count != m_Nodes.Count)
        {
            if(iVerbose)
                Debug.LogError("Graph is not in one piece");

            return false;
        }

        return true;
    }

    private void _VisitNode(Node iNode, HashSet<Node> iVisitedNodes)
    {
        if(iVisitedNodes.Contains(iNode))
            return;

        iVisitedNodes.Add(iNode);
        foreach(Node neighbour in iNode.GetNeighbours())
            _VisitNode(neighbour, iVisitedNodes);
    }

    public void DestroyGraph()
    {
        foreach(Node node in m_Nodes)
            node.ResetNode();

        m_Nodes.Clear();
        m_MaxValue = -1;

        OnPendingGraphDesctruction.Invoke();
    }
    
    // completed if the nodes have the same value
    public bool CheckGraphCompletion()
    {
        if(m_MaxValue < 0 || m_Nodes.Count <= 0 || !hasBeenInit) // not initialized
            return false;

        int testValue = m_Nodes[0].GetValue();
        foreach(Node node in m_Nodes)
        {
            if(testValue != node.GetValue())
                return false;
        }

        return true;
    }

    public int GetNbMove()
    {
        return m_NbMove;
    }

    public void AddMoveNumber()
    {
        m_NbMove++;
    }

    public List<Node> GetNodes()
    {
        return m_Nodes;
    }
}
