using UnityEngine;
using System.Collections;
using System;

public class ProjectileHandler : MonoBehaviour
{
    string[] tags = new string[] { "Player", "Projectile" };
    GameObject stuckObject;
    bool stuck;

    void Update()
    {
        Vector2 moveDirection = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        stuck = true;
        for (int i = 0; i < tags.Length; i++)
        {
            if (collision.tag == tags[i])
            {
                i = tags.Length;
                stuck = false;
            }
        }
        if(stuck)
        {
            stuck = collision.gameObject;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == stuckObject)
        {
            stuck = false;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }
}
