using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeController : MonoBehaviour
{
    [SerializeField] Button m_Button;

    private Node m_Node;

    // Start is called before the first frame update
    void Start()
    {
        m_Button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void OnClick()
    {
        if(m_Node == null)
        {
            if(!TryGetComponent<Node>(out m_Node))
                return;
        }

        m_Node.NextValueOnNeighbours();
    }
}
