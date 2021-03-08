using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBallBehaviour : MonoBehaviour
{
    [SerializeField]
    private MovementBehaviour m_movement;

    [SerializeField]
    private PyramidGraph m_Pyramid;

    private PyramidNode m_currentNode;
    private PyramidNode m_destNode;

    private float m_fMoveTimer;
    private float m_fMoveTimerCurrent;

    // Start is called before the first frame update
    void Start()
    {
        m_movement = GetComponent<MovementBehaviour>();
        m_Pyramid = GameObject.Find("Pyramid").GetComponent<PyramidGraph>();

        m_fMoveTimer = 0.3f;
        m_fMoveTimerCurrent = 0.0f;

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_movement.CheckCompleted())
        {
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

    void Spawn()
    {
        int spawnSide = Random.Range(0, 2);

        if (spawnSide == 0)
        {
            // Spawn on left side
            m_destNode = m_Pyramid.Nodes[1];

            Vector3[] newPath = new Vector3[4];

            Vector3 spawnPos = m_destNode.m_Position + new Vector3(0.0f, 10.0f, 0.0f);
            transform.position = spawnPos;

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
            transform.position = spawnPos;

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
    void Death()
    {
        Destroy(gameObject);
    }
}
