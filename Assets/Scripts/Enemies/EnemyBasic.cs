using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{

    public Transform waypointParent;

    private Transform[] points;
    private int currentWaypoint = 1;

    public float waypointDistance = 0.6f;
    public float speed = 1f;

    public Transform enemy;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        points = waypointParent.GetComponentsInChildren<Transform>();
    }

    void OnDrawGizmos()
    {
        points = waypointParent.GetComponentsInChildren<Transform>();
        if (points != null)
        {
            Gizmos.color = Color.red;

            for (int i = 1; i < points.Length - 1; i++)
            {
                Transform pointA = points[i];
                Transform pointB = points[i + 1];
                Gizmos.DrawLine(pointA.position, pointB.position);
            }

            for (int i = 1; i < points.Length; i++)
            {
                Gizmos.DrawSphere(points[i].position, waypointDistance);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get current waypoint
        Transform currentPoint = points[currentWaypoint];

        //Move towards current waypoint
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);
        //Check if distance between waypoint is close
        float distance = Vector3.Distance(transform.position, currentPoint.position);
        //Switch to next waypoint
        if (distance < waypointDistance)
        {
            currentWaypoint++;
        }

        if (currentWaypoint == points.Length)
        {
            currentWaypoint = 1;
        }


        //
    }
}

