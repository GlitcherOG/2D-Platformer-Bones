using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    // Parent object of the waypoints (making it easy to place and change)
    public Transform waypointParent;

    // array to store any point within the Waypoint parent
    private Transform[] points;
    // set default waypoint
    private int currentWaypoint = 1;
    // is enemy dying?
    bool dying = false;
    // the didtance of the way point the enemy needs to get to before it accepts that it hit the waypoint
    public float waypointDistance = 0.6f;
    // move speed of enemy
    public float speed = 1f;
    
    public bool dead;

    // quick fix used to flip namimation when changing direction
    public bool facingRight;

    // enemy sprite renderer
    public SpriteRenderer spriteRenderer;
    // the actual enemy object
    public GameObject enemy;
    // enemy animator
    public Animator anim;

    public float delayTime = 1f;
    
    void Start()
    {
        // get the points from the parentWaypoint
        points = waypointParent.GetComponentsInChildren<Transform>();
        // get sprite renderer from object script is attached to
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    //Function to draw spheres the diameter of the waypointDistance around the waypoint point for easier placement (only shows in the editor
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
                Gizmos.DrawSphere(points[i].position, 0.2f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get current waypoint
        Transform currentPoint = points[currentWaypoint];

        if(dead==true)
        {
            // if dead destroy object
            Destroy(this.gameObject);
        }

        if (dying == false)
        {
            //Move towards current waypoint
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);
        }

        // check enemy movement direction
        if (currentPoint.position.x > transform.position.x)
        {
            // flip accordingly
            spriteRenderer.flipX = true;
        }
        else
        {
            // flip accordingly
            spriteRenderer.flipX = false;
        }


        //Check if distance between waypoint is close
        float distance = Vector3.Distance(transform.position, currentPoint.position);

        //Switch to next waypoint
        if (distance < waypointDistance)
        {
            //there are still waypoints available in the list, go to the next
            currentWaypoint++;
        }

        if (currentWaypoint == points.Length)
        {
            // current waypoint is last waypoint return to the first and start again
            currentWaypoint = 1;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // if a projectile hits obeject 
        if(collision.tag == "Projectile")
        {
            // set death animation and trigger
            dying = true;
            anim.SetTrigger("Dead");
        }
    }
}

