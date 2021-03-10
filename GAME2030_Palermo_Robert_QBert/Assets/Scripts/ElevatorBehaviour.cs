using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBehaviour : MonoBehaviour
{
    [SerializeField]
    private PlayerController m_player;

    [SerializeField]
    private PyramidNode m_currentNode;

    [SerializeField]
    private MovementBehaviour m_movement;

    [SerializeField]
    private PyramidGraph m_Pyramid;

    private bool m_Started;

    // Start is called before the first frame update
    void Start()
    {
        m_Started = false;

        m_currentNode.m_nodeType = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_currentNode.m_nodeType != 2 && !m_Started)
        {
            m_currentNode.m_nodeType = 2;
        }

        if(!m_Started && m_player.m_currentNode == m_currentNode)
        {
            m_Started = true;
            StartElevator();
        }

        if(m_movement.bPathRunning)
        {
            m_player.m_bAllowMovement = false;
            m_player.transform.position = transform.position + new Vector3(0.0f, 0.4f, 0.0f);
        }
        else
        {
            m_player.m_bAllowMovement = true;
        }

        if(m_movement.CheckCompleted())
        {
            m_player.m_destNode = m_Pyramid.Nodes[0];

            Vector3[] newPath = new Vector3[4];

            newPath[0] = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
            newPath[1] = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
            newPath[2] = m_Pyramid.Nodes[0].m_Position + new Vector3(0.0f, 0.5f, 0.0f);
            newPath[3] = m_Pyramid.Nodes[0].m_Position;

            m_player.m_movement.SetPath(newPath);

            m_player.m_movement.bPathStart = true;

            // Destroy this Object
            Destroy(gameObject);
        }
    }

    public void StartElevator()
    {
        Vector3[] newPath = new Vector3[4];

        m_movement.fSpeedFactor = 0.5f;

        newPath[0] = m_currentNode.m_Position;
        newPath[1] = m_currentNode.m_Position;
        newPath[2] = m_Pyramid.Nodes[0].m_Position + new Vector3(0.0f, 0.6f, 0.0f);
        newPath[3] = m_Pyramid.Nodes[0].m_Position + new Vector3(0.0f, 0.6f, 0.0f);

        m_movement.SetPath(newPath);

        m_movement.bPathStart = true;

        m_currentNode.m_nodeType = 1;
    }
}
