using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    public Vector3[] path;

    public bool bPathStart;
    public bool bPathRunning;
    public bool bPathComplete;

    private float fTime;

    private Vector3 vPosition;

    private float fSpeedFactor;

    private void Start()
    {
        bPathRunning = false;
        fTime = 0.0f;
        fSpeedFactor = 1.0f;

        for (int i = 0; i < 4; i++)
        {
            path[i] = Vector3.zero;
        }
    }

    private void Update()
    {
        if (bPathRunning == false && bPathStart == true)
        {
            StartCoroutine(Route());
        }
    }

    private IEnumerator Route()
    {
        bPathStart = false;
        bPathRunning = true;
        bPathComplete = false;

        Vector3 p0 = path[0];
        Vector3 p1 = path[1];
        Vector3 p2 = path[2];
        Vector3 p3 = path[3];

        while(fTime < 1)
        {
            fTime += Time.deltaTime * fSpeedFactor;

            vPosition = Mathf.Pow(1 - fTime, 3) * p0 +
                3 * Mathf.Pow(1 - fTime, 2) * fTime * p1 +
                3 * Mathf.Pow(fTime, 2) * (1 - fTime) * p2 +
                Mathf.Pow(fTime, 3) * p3;

            transform.position = vPosition;
            yield return new WaitForEndOfFrame();
        }

        fTime = 0.0f;
        

        bPathRunning = false;
        bPathComplete = true;
    }

    public bool CheckCompleted()
    {
        if(bPathComplete)
        {
            bPathComplete = false;
            return true;
        }

        return false;
    }

    /*
    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * path[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * path[1].position +
                3 * Mathf.Pow(t, 2) * (1 - t) * path[2].position +
                Mathf.Pow(t, 3) * path[3].position;

            Gizmos.DrawSphere(gizmosPosition, 0.1f);
        }

        Gizmos.DrawLine((path[0].position), path[1].position);
        Gizmos.DrawLine((path[2].position), path[3].position);

    }
    */
}
