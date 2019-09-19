using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed = 5;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector2(projectileSpeed, 0));
                
                //AddForce(transform.forward * 10);
        }
    }

}
