using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

using ChangedValueEvent = UnityEngine.Events.UnityEvent<int>;

public class Node : MonoBehaviour
{
    // called with old value as parameter
    public ChangedValueEvent OnValueChanged;
    public UnityEngine.Events.UnityEvent OnNodeInit;

    // values should be within positive or null
    // negative value means unitialized
    private int m_Val = -1;

    private HashSet<Node> m_Neighbours = new HashSet<Node>();

    private Graph m_Graph = null;

    private void Awake()
    {
        if(OnValueChanged == null)
            OnValueChanged = new ChangedValueEvent();
        if(OnNodeInit == null)
            OnNodeInit = new UnityEngine.Events.UnityEvent();
    }

    public void SetValue(int iNewVal)
    {
        int oldVal = m_Val;
        m_Val = iNewVal;
        OnValueChanged.Invoke(oldVal);
    }

    private void _SetGraph(Graph iGraph)
    {
        m_Graph = iGraph;
    }

    public void Init(Graph iGraph)
    {
        Assert.IsNull(m_Graph);
        _SetGraph(iGraph);
        SetValue(0);
        OnNodeInit.Invoke();
    }

    private void AddNeighbour(Node iNewNeighbour)
    {
        m_Neighbours.Add(iNewNeighbour);
    }

    public Graph GetGraph()
    {
        return m_Graph;
    }

    public HashSet<Node> GetNeighbours()
    {
        return m_Neighbours;
    }

    public int GetValue()
    {
        return m_Val;
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
        m_Graph.AddMoveNumber();

        foreach(Node neighbour in m_Neighbours)
            neighbour.NextValue();

        if(m_Graph.CheckGraphCompletion())
            m_Graph.OnGraphCompletion.Invoke(m_Graph.GetNbMove());
    }

    // we ensure that:
    // * there is no self linked node
    // * all links are non-directionnal
    public bool CheckNodeIntegrity(bool iVerbose = true)
    {
        if(m_Neighbours.Contains(this))
        {
            if(iVerbose)
                Debug.LogError("Node is self linked");

            return false;
        }

        if(m_Neighbours.Count <= 0)
        {
            if(iVerbose)
                Debug.LogError("Node has no neighbour");

            return false;
        }

        foreach(Node neighbour in m_Neighbours)
        {
            if(!neighbour.HasNeighbour(this))
            {
                if(iVerbose)
                    Debug.LogError("Node has a one-sided link");

                return false;
            }
        }

        return true;
    }

    public bool HasNeighbour(Node iPotentialNeighbour)
    {
        return m_Neighbours.Contains(iPotentialNeighbour);
    }

    public void ResetNode()
    {
        int oldVal = m_Val;
        m_Val = -1;
        m_Neighbours.Clear();
        m_Graph = null;

        OnValueChanged.Invoke(oldVal);
    }
}
