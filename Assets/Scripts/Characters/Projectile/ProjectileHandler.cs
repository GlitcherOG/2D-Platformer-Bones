using UnityEngine;
using System.Collections;
using System;

public class ProjectileHandler : MonoBehaviour
{
    string[] tags = new string[] { "Player", "Projectile" }; //Tags for what to stop the arrow at
    private bool stuck; //Projectile is stuck in object

    void Update()
    {
        //get rigid body movment direction        
        Vector2 moveDirection = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (moveDirection != Vector2.zero) // if rigid body is moving
        {
            // get direction of movement
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            // rotate rigid body toward direction it is moving
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    //Runs on trigger enter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Sets stuck bool to true
        stuck = true;
        //Goes through all tags
        for (int i = 0; i < tags.Length; i++)
        {
            //If the collision tag is equal to the tag set stuck to false
            if (collision.tag == tags[i])
            {
                //Changes stuck to false
                stuck = false;
            }
        }
        //If stuck is true run code below
        if (stuck)
        {
            //Set the projectiles parent to the collided object
            gameObject.transform.SetParent(collision.gameObject.transform);
            //Get the projectiles rigidbody and freeze the position of it
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            //Disable the collider of the arrow
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
