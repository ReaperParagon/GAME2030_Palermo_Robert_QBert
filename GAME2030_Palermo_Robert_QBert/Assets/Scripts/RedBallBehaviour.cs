using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBallBehaviour : MonoBehaviour
{
    [SerializeField]
    private MovementBehaviour m_movement;

    [SerializeField]
    private PyramidGraph m_Pyramid;

    [SerializeField]
    private Animator m_animator;

    private PyramidNode m_currentNode;
    private PyramidNode m_destNode;

    private float m_fMoveTimer;
    private float m_fMoveTimerCurrent;

    private bool m_bAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        m_movement = GetComponent<MovementBehaviour>();
        m_Pyramid = GameObject.Find("Pyramid").GetComponent<PyramidGraph>();
        m_animator = GetComponent<Animator>();

        m_fMoveTimer = 0.3f;
        m_fMoveTimerCurrent = 0.0f;

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimations();

        if (m_movement.CheckCompleted())
        {
            if (!m_bAlive)
            {
                Destroy(gameObject);
            }

            m_currentNode = m_destNode;
        }

        if (m_currentNode != null && m_currentNode.m_nodeType == 1)
        {
            Death();
        }

        if (!m_movement.bPathRunning)
        {
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

    void UpdateAnimations()
    {
        m_animator.SetBool("Pathing", m_movement.bPathRunning);
    }

    void Spawn()
    {
        int spawnSide = Random.Range(0, 2);

        if(spawnSide == 0)
        {
            // Spawn on left side
            m_destNode = m_Pyramid.Nodes[1];

            Vector3[] Path = new Vector3[4];

            Vector3 spawnPos = m_destNode.m_Position + new Vector3(0.0f, 10.0f, 0.0f);

            Path[0] = spawnPos;
            Path[1] = spawnPos + new Vector3(0.0f, 0.6f, 0.0f);
            Path[2] = m_destNode.m_Position + new Vector3(0.0f, 0.6f, 0.0f);
            Path[3] = m_destNode.m_Position;

            m_movement.SetPath(Path);
        }
        else
        {
            // Spawn on right side
            m_destNode = m_Pyramid.Nodes[2];

            Vector3[] Path = new Vector3[4];

            Vector3 spawnPos = m_destNode.m_Position + new Vector3(0.0f, 10.0f, 0.0f);

            Path[0] = spawnPos;
            Path[1] = spawnPos + new Vector3(0.0f, 0.6f, 0.0f);
            Path[2] = m_destNode.m_Position + new Vector3(0.0f, 0.6f, 0.0f);
            Path[3] = m_destNode.m_Position;

            m_movement.SetPath(Path);
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
