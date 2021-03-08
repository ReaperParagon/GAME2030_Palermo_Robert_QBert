using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Coily;

    [SerializeField]
    private GameObject m_RedBall;

    [SerializeField]
    private GameObject m_GreenBall;

    private float m_fTimer;
    private float m_fTimerCurrent;

    private bool m_bCoilySpawned;
    private int m_iEnemySpawns;

    // Start is called before the first frame update
    void Start()
    {
        m_fTimer = Random.Range(3.0f, 7.0f);
        m_fTimerCurrent = 0.0f;

        m_bCoilySpawned = false;
        m_iEnemySpawns = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_fTimerCurrent >= m_fTimer)
        {
            // Timer is done
            // Change next spawn time
            m_fTimer = Random.Range(3.0f, 7.0f);

            // Reset current spawn timer
            m_fTimerCurrent = 0.0f;

            // Spawn
            Spawn();
        }
        else
        {
            m_fTimerCurrent += Time.deltaTime;
        }
    }

    void Spawn()
    {
        // Decide which entity to spawn
        float range = Random.Range(0.0f, 1.0f);
        int enemyToSpawn = 0;

        if(range > 0.2f && range <= 0.7f)
        {
            enemyToSpawn = 1;
        }
        else if(range > 0.7f)
        {
            enemyToSpawn = 2;
        }

        // Check if Coily is already spawned
        if(m_bCoilySpawned)
        {
            if(enemyToSpawn == 0)
            {
                // Change Enemy to spawn from coily to red ball
                enemyToSpawn = 1;
            }
        }
        else
        {
            // Check if Coily needs to be spawned (Within 3 enemy spawns)
            if(m_iEnemySpawns >= 2)
            {
                // Spawn Coily
                enemyToSpawn = 0;
            }
            else
            {
                m_iEnemySpawns++;
            }
        }

        // Spawn The Enemy
        switch(enemyToSpawn)
        {
            case 0:
                // Coily
                m_iEnemySpawns = 0;
                m_bCoilySpawned = true;
                Instantiate(m_Coily);
                break;
            case 1:
                // Red Ball
                Instantiate(m_RedBall);
                break;
            case 2:
                // Green Ball
                Instantiate(m_GreenBall);
                break;
        }
    }

    public void CoilyDespawned()
    {
        m_bCoilySpawned = false;
    }
}
