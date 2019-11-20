using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameShip : MonoBehaviour
{
     
    void Update()
    {
        //get rigid body movment direction        
        /*Vector2 moveDirection = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (moveDirection != new Vector2(0.02f, 0)) // if rigid body is moving right
        {
            // get direction of movement
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            // rotate rigid body toward direction it is moving
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        }*/

        

    }
}
