using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectile;
    public GameObject projectilePrefab;

    public float projectileSpeed = 5;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (Vector2)((worldMousePos - transform.position));
            //direction.Normalize();
            //Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            //transform.TransformDirection(new Vector2(projectileSpeed, 0));
            //direction * projectileSpeed;


            StartCoroutine(RemoveProjectile(bullet, 5f));
            //AddForce(transform.forward * 10);
        }
    }

   
    IEnumerator RemoveProjectile(GameObject bullet, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(bullet);
        // Now do your thing here
    }



}
