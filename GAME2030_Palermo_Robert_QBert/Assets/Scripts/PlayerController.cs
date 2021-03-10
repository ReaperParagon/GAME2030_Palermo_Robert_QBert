using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;

    [SerializeField]
    public MovementBehaviour m_movement;

    [SerializeField]
    private PauseMenuBehaviour m_pauseMenuBehaviour;

    [SerializeField]
    private Animator m_animator;

    private bool m_bIsAlive = true;

    [SerializeField]
    private PyramidGraph m_Pyramid;

    public PyramidNode m_currentNode;
    public PyramidNode m_destNode;

    public bool m_bAllowMovement = true;
    private bool m_bBack = false;

    // Start is called before the first frame update
    void Start()
    {
        m_currentNode = m_Pyramid.Nodes[0];
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        UpdateAnimations();

        // Handle Activating The current Node
        if(m_movement.CheckCompleted())
        {
            m_bAllowMovement = true;
            m_currentNode = m_destNode;

            if(!m_currentNode.bIsActivated)
            {
                m_currentNode.bIsActivated = true;
                m_currentNode.ChangeSprite();
            }
        }
    }

    void UpdateAnimations()
    {
        m_animator.SetBool("Pathing", m_movement.bPathRunning);
        m_animator.SetBool("Back", m_bBack);
    }

    void HandleInput()
    {
        if(m_bAllowMovement && !m_pauseMenuBehaviour.m_paused)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Keypad7))
            {
                MoveTo(m_currentNode.m_TopLeft);
                GetComponent<SpriteRenderer>().flipX = false;
                m_bBack = true;
            }

            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Keypad9))
            {
                MoveTo(m_currentNode.m_TopRight);
                GetComponent<SpriteRenderer>().flipX = true;
                m_bBack = true;
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                MoveTo(m_currentNode.m_BotLeft);
                GetComponent<SpriteRenderer>().flipX = false;
                m_bBack = false;
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                MoveTo(m_currentNode.m_BotRight);
                GetComponent<SpriteRenderer>().flipX = true;
                m_bBack = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(m_pauseMenuBehaviour.m_paused)
            {
                m_pauseMenuBehaviour.Resume();
            }
            else
            {
                m_pauseMenuBehaviour.ShowPauseMenu();
            }
        }
    }

    void MoveTo(PyramidNode dest)
    {
        // CHANGE FOR DEATH STATE AND ELEVATORS
        if(dest != null)
        {
            if(dest.m_nodeType == 1)
            {
                // Void node, fall off level
                m_bIsAlive = false;
                m_currentNode = m_Pyramid.Nodes[0];
                m_Player.transform.position = m_currentNode.m_Position;
            }
            else if(m_movement.bPathRunning == false)
            {
                // Set Movement Path Points
                Vector3[] newPath = new Vector3[4];

                newPath[0] = m_currentNode.m_Position;
                newPath[1] = m_currentNode.m_Position + new Vector3(0.0f, 0.6f, 0.0f);
                newPath[2] = dest.m_Position + new Vector3(0.0f, 0.6f, 0.0f);
                newPath[3] = dest.m_Position;

                m_movement.SetPath(newPath);

                // Run Movement Script
                m_movement.bPathStart = true;

                // m_Player.transform.position = dest.m_Position;
                m_destNode = dest;

                m_bAllowMovement = false;
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
