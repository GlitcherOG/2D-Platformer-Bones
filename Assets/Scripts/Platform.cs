using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform waypointParent;
    private Transform[] points;
    private int currentWaypoint = 2;
    public Switch toggle;
    public float waypointDistance = 0.6f;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        points = waypointParent.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle.toggle)
        {
            Transform currentPoint = points[currentWaypoint];
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, currentPoint.position);
            if (distance < waypointDistance)
            {
                currentWaypoint++;
            }

            if (currentWaypoint == points.Length)
            {
                currentWaypoint = 1;
            }
        }
    }
}
