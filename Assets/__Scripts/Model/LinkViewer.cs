using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Node))]
public class LinkViewer : MonoBehaviour
{
    private Node m_Node;
    private List<GameObject> m_LinksView = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        m_Node = GetComponent<Node>();
        m_Node.OnNodeInit.AddListener(OnInitNode);
    }

    void OnInitNode()
    {
        m_Node.GetGraph().OnInitGraph.AddListener(CreateLinks);
    }

    void CreateLinks()
    {
        _DestroyLinks();

        foreach (Node neighbour in m_Node.GetNeighbours())
        {
            // ensuring only one node of the link will create a cylinder
            if (neighbour.GetInstanceID() > m_Node.GetInstanceID())
                continue;

            Vector3 deltaPos = neighbour.transform.position - transform.position;
            if (deltaPos.magnitude <= 0)
                continue;

            GameObject linkView = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

            // calculting cylinder orientation
            Vector3 newForward = Vector3.forward; // we just want any vector orthogonal to deltaPos (since it is a cylinder)
            if (deltaPos.normalized != Vector3.up)
                newForward = Vector3.Cross(deltaPos.normalized, Vector3.up);
            else
                newForward = Vector3.Cross(deltaPos.normalized, Vector3.right);

            linkView.transform.LookAt(linkView.transform.position + newForward, deltaPos.normalized);
            linkView.transform.position = (neighbour.transform.position + transform.position) * 0.5f;
            linkView.transform.localScale = new Vector3(transform.localScale.x / 2, deltaPos.magnitude * 0.5f, transform.localScale.z / 2); // assuming scale to be uniform (x = z)

            m_LinksView.Add(linkView);
        }
    }

    private void _DestroyLinks()
    {
        foreach (GameObject link in m_LinksView)
            Destroy(link);
    
        m_LinksView.Clear();
    }


    void OnDestroy()
    {
        _DestroyLinks();
    }
}
