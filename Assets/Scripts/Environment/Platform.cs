using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform waypointParent; //Waypoint travel system parent
    private Transform[] points; //waypoint points
    private int currentWaypoint = 2; //Whats the current waypoint
    public Switch toggle; //Switch script for toggle
    public float waypointDistance = 0.6f; //Waaypoint distance
    public float speed = 1f; //Speed of platform

    void Start()
    {
        //Get all waypoints and set them into the point variables
        points = waypointParent.GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        //If script toggle is true move platform
        if (toggle.toggle)
        {
            //Sets the transformation of the cuurrent waypoint
            Transform currentPoint = points[currentWaypoint];
            //move the transformation of the platform towards the waypoint at the speed of float speed
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);
            //get the distance for the current location in relation of the platform at waypoint
            float distance = Vector3.Distance(transform.position, currentPoint.position);
            //if the distance is less than the waypoint distance go to next waypoint
            if (distance < waypointDistance)
            {
                currentWaypoint++;
            }
            //if the current waypoint is greater than the ammount of points
            if (currentWaypoint == points.Length)
            {
                currentWaypoint = 1;
            }
        }
    }
}
