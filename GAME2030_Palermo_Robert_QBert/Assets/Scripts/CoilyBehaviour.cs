using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilyBehaviour : MonoBehaviour
{
    [SerializeField]
    private PlayerController m_player;

    [SerializeField]
    private MovementBehaviour m_movement;

    [SerializeField]
    private PyramidGraph m_Pyramid;

    [SerializeField]
    private Animator m_animator;

    private EnemyManager m_enemyManager;

    private PyramidNode m_currentNode;
    private PyramidNode m_destNode;

    private float m_fMoveTimer;
    private float m_fMoveTimerCurrent;

    private float m_fHatchTimer;
    private float m_fHatchTimerCurrent;

    private bool m_bHatched;
    private bool m_bLeft = true;
    private bool m_bBack = false;
    private bool m_bAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        m_movement = GetComponent<MovementBehaviour>();
        m_Pyramid = GameObject.Find("Pyramid").GetComponent<PyramidGraph>();
        m_player = GameObject.Find("QBert").GetComponent<PlayerController>();
        m_enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        m_animator = GetComponent<Animator>();

        m_fMoveTimer = 0.3f;
        m_fMoveTimerCurrent = 0.0f;

        m_fHatchTimer = 1.0f;
        m_fHatchTimerCurrent = 0.0f;

        m_bHatched = false;

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimations();

        if (m_movement.CheckCompleted())
        {
            if(!m_bAlive)
            {
                m_enemyManager.CoilyDespawned();
                Destroy(gameObject);
            }

            m_currentNode = m_destNode;
        }

        if (m_currentNode != null && m_currentNode.m_nodeType == 1 && m_bAlive)
        {
            Death();
        }

        if (m_bHatched)
        {
            // Snake Form
            if (!m_movement.bPathRunning)
            {
                if (m_fMoveTimerCurrent >= m_fMoveTimer)
                {
                    m_fMoveTimerCurrent = 0.0f;

                    PathFinding();
                }
                else
                {
                    m_fMoveTimerCurrent += Time.deltaTime;
                }
            }
        }
        else
        {
            // Egg Form
            if (!m_movement.bPathRunning)
            {
                // Check if on the bottom row
                if (m_currentNode.m_iIndex >= 22 && m_currentNode.m_iIndex <= 28)
                {
                    // Hatch the egg
                    if (m_fHatchTimerCurrent >= m_fHatchTimer)
                    {
                        m_fHatchTimerCurrent = 0.0f;

                        m_bHatched = true;
                    }
                    else
                    {
                        m_fHatchTimerCurrent += Time.deltaTime;
                    }
                }
                else
                {
                    // Try to get to the bottom row
                    if (m_fMoveTimerCurrent >= m_fMoveTimer)
                    {
                        m_fMoveTimerCurrent = 0.0f;

                        RandomMovement();
                    }
                    else
                    {
                        m_fMoveTimerCurrent += Time.deltaTime;
                    }
                }
            }
        }
    }

    void UpdateAnimations()
    {
        m_animator.SetBool("Hatched", m_bHatched);
        m_animator.SetBool("Left", m_bLeft);
        m_animator.SetBool("Back", m_bBack);
        m_animator.SetBool("Pathing", m_movement.bPathRunning);
    }

    void Spawn()
    {
        int spawnSide = Random.Range(0, 2);

        if (spawnSide == 0)
        {
            // Spawn on left side
            m_destNode = m_Pyramid.Nodes[1];

            Vector3[] newPath = new Vector3[4];

            Vector3 spawnPos = m_destNode.m_Position + new Vector3(0.0f, 10.0f, 0.0f);

            newPath[0] = spawnPos;
            newPath[1] = spawnPos + new Vector3(0.0f, 0.6f, 0.0f);
            newPath[2] = m_destNode.m_Position + new Vector3(0.0f, 0.6f, 0.0f);
            newPath[3] = m_destNode.m_Position;

            m_movement.SetPath(newPath);
        }
        else
        {
            // Spawn on right side
            m_destNode = m_Pyramid.Nodes[2];

            Vector3[] newPath = new Vector3[4];

            Vector3 spawnPos = m_destNode.m_Position + new Vector3(0.0f, 10.0f, 0.0f);

            newPath[0] = spawnPos;
            newPath[1] = spawnPos + new Vector3(0.0f, 0.6f, 0.0f);
            newPath[2] = m_destNode.m_Position + new Vector3(0.0f, 0.6f, 0.0f);
            newPath[3] = m_destNode.m_Position;

            m_movement.SetPath(newPath);
        }

        m_movement.bPathStart = true;
    }

    void RandomMovement()
    {
        int moveSide = Random.Range(0, 2);

        if (moveSide == 0)
        {
            // Spawn on left side
            PyramidNode destNode = m_currentNode.m_BotLeft;

            Vector3[] newPath = new Vector3[4];

            newPath[0] = m_currentNode.m_Position;
            newPath[1] = m_currentNode.m_Position + new Vector3(0.0f, 0.6f, 0.0f);
            newPath[2] = destNode.m_Position + new Vector3(0.0f, 0.6f, 0.0f);
            newPath[3] = destNode.m_Position;

            m_movement.SetPath(newPath);

            m_destNode = destNode;
        }
        else
        {
            // Spawn on right side
            PyramidNode destNode = m_currentNode.m_BotRight;

            Vector3[] newPath = new Vector3[4];

            newPath[0] = m_currentNode.m_Position;
            newPath[1] = m_currentNode.m_Position + new Vector3(0.0f, 0.6f, 0.0f);
            newPath[2] = destNode.m_Position + new Vector3(0.0f, 0.6f, 0.0f);
            newPath[3] = destNode.m_Position;

            m_movement.SetPath(newPath);

            m_destNode = destNode;
        }

        m_movement.bPathStart = true;
    }

    void PathFinding()
    {
        PyramidNode destNode;
        Vector3 PlayerPosition = m_player.m_currentNode.m_Position;

        if (PlayerPosition.x >= transform.position.x)
        {
            m_bLeft = false;
            // Move Right
            if(PlayerPosition.y >= transform.position.y)
            {
                // Move Up
                destNode = m_currentNode.m_TopRight;
                m_bBack = true;
            }
            else
            {
                // Move Down
                if(m_currentNode.m_BotRight.m_nodeType == 1)
                {
                    destNode = m_currentNode.m_TopRight;
                    m_bBack = true;
                }
                else
                {
                    destNode = m_currentNode.m_BotRight;
                    m_bBack = false;
                }
            }
        }
        else
        {
            m_bLeft = true;
            // Move Left
            if (PlayerPosition.y >= transform.position.y)
            {
                // Move Up
                destNode = m_currentNode.m_TopLeft;
                m_bBack = true;
            }
            else
            {
                // Move Down
                if (m_currentNode.m_BotLeft.m_nodeType == 1)
                {
                    destNode = m_currentNode.m_TopLeft;
                    m_bBack = true;
                }
                else
                {
                    destNode = m_currentNode.m_BotLeft;
                    m_bBack = false;
                }
            }
        }

        // int m_destIndex = m_Pyramid.BreadthFirstSearch(m_currentNode.m_iIndex, m_player.m_currentNode.m_iIndex);

        // PyramidNode m_destNode = m_Pyramid.Nodes[m_destIndex];
        
        Vector3[] newPath = new Vector3[4];

        newPath[0] = m_currentNode.m_Position;
        newPath[1] = m_currentNode.m_Position + new Vector3(0.0f, 0.6f, 0.0f);
        newPath[2] = destNode.m_Position + new Vector3(0.0f, 0.6f, 0.0f);
        newPath[3] = destNode.m_Position;

        m_movement.SetPath(newPath);

        m_destNode = destNode;
        m_movement.bPathStart = true;
    }

    void Death()
    {
        // Order in Layer
        GetComponent<SpriteRenderer>().sortingOrder = -2;

        // Start the path to death
        Vector3[] newPath = new Vector3[4];

        Vector3 fallOffPos = m_currentNode.m_Position + new Vector3(0.0f, 0.0f, -m_currentNode.m_Position.z + 0.5f);

        newPath[0] = fallOffPos;
        newPath[1] = fallOffPos;
        newPath[2] = m_currentNode.m_Position + new Vector3(0.0f, -15.0f, 0.5f);
        newPath[3] = m_currentNode.m_Position + new Vector3(0.0f, -15.0f, 0.5f);

        m_movement.SetPath(newPath);
        m_movement.fSpeedFactor = 0.7f;

        // m_destNode = destNode;
        m_movement.bPathStart = true;
        m_bAlive = false;
    }
}
