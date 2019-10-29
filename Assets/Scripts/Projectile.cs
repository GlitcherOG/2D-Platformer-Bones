using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectile;

    public float projectileSpeed = 5;
    public float projectileDestroyTime = 5f;

    void Update()
    {
        // get the mouse position in the game
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane *10));

        // 
        transform.LookAt(worldMousePos);

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // Create a new projectile instance
            GameObject bullet = Instantiate(projectile, transform.position, transform.rotation) as GameObject;

            // Give the projectile 'a boost' toward the mouse position (fire it)
            bullet.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector3(0, 0, projectileSpeed * 5));

            // Destroy projectile after x seconds so they don't stack up and take up resources
            StartCoroutine(RemoveProjectile(bullet, projectileDestroyTime));
        }
    }

   
    IEnumerator RemoveProjectile(GameObject bullet, float delayTime)
    {
        // Start a delay on projectile instance
        yield return new WaitForSeconds(delayTime);

        // Remove projectile
        Destroy(bullet);
    }



}
