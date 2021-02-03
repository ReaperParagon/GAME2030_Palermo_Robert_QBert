using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;

    private bool m_bIsAlive = true;

    [SerializeField]
    private PyramidGraph m_Pyramid;

    private PyramidNode m_currentNode;

    // Start is called before the first frame update
    void Start()
    {
        m_currentNode = m_Pyramid.Nodes[0];
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            MoveTo(m_currentNode.m_TopLeft);
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            MoveTo(m_currentNode.m_TopRight);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            MoveTo(m_currentNode.m_BotLeft);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            MoveTo(m_currentNode.m_BotRight);
        }
    }

    void MoveTo(PyramidNode dest)
    {
        // CHANGE FOR DEATH STATE AND ELEVATORS
        if(dest != null)
        {
            m_Player.transform.position = dest.m_Position;
            m_currentNode = dest;

            // Activate the Node
            if(!m_currentNode.bIsActivated)
            {
                m_currentNode.bIsActivated = true;
                // m_currentNode.ChangeSprite();
            }
        }
        else
        {
            m_bIsAlive = false;
            m_currentNode = m_Pyramid.Nodes[0];
            m_Player.transform.position = m_currentNode.m_Position;
        }
        
    }
}
