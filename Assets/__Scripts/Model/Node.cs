using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using ChangedValueEvent = UnityEngine.Events.UnityEvent<int>;

public class Node : MonoBehaviour
{
    // called with old value as parameter
    public ChangedValueEvent OnValueChanged;

    // values should be within positive or null
    // negative value means unitialized
    private int m_Val = -1;

    private HashSet<Node> m_Neighbours = null;

    private Graph m_Graph;

    private void Awake()
    {
        if (OnValueChanged == null)
            OnValueChanged = new ChangedValueEvent();
    }

    public void SetValue(int iNewVal)
    {
        int oldVal = m_Val;
        m_Val = iNewVal;
        OnValueChanged.Invoke(oldVal);
    }

    private void SetGraph(Graph iGraph)
    {
        m_Graph = iGraph;
    }

    public void Init(Graph iGraph)
    {
        m_Neighbours = new HashSet<Node>();
        SetGraph(iGraph);
        SetValue(0);
    }

    private void AddNeighbour(Node iNewNeighbour)
    {
        m_Neighbours.Add(iNewNeighbour);
    }
        
    public static void AddLink(Node iNode1, Node iNode2)
    {
        Assert.AreEqual(iNode1.GetGraph(), iNode2.GetGraph(), "Can't link nodes that do not belong to the same graph");
        iNode1.AddNeighbour(iNode2);
        iNode2.AddNeighbour(iNode1);
    }

    public void NextValue()
    {
        if(m_Val == m_Graph.GetMaxNodeValue())
            SetValue(0);
        else
            SetValue(m_Val+1);
    }

    public void NextValueOnNeighbours()
    {
        foreach(Node neighbour in m_Neighbours)
            neighbour.NextValue();
    }

    // we ensure that:
    // * there is no self linked node 
    // * all links are non-directionnal
    public bool CheckNodeIntegrity()
    {
        if (m_Neighbours.Contains(this))
            return false;

        if (m_Neighbours.Count <= 0)
            return false;

        foreach(Node neighbour in m_Neighbours)
        {
            if(!neighbour.HasNeighbour(this))
                return false;
        }

        return true;
    }

    public bool HasNeighbour(Node iPotentialNeighbour)
    {
        return m_Neighbours.Contains(iPotentialNeighbour);
    }

    public Graph GetGraph()
    {
        return m_Graph;
    }

    public HashSet<Node> GetNeighbours()
    {
        return m_Neighbours;
    }
}
