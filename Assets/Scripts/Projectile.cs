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
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane *10));
        transform.LookAt(worldMousePos);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GameObject bullet = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector3(0, 0, projectileSpeed));
            StartCoroutine(RemoveProjectile(bullet, 5f));
        }
    }

   
    IEnumerator RemoveProjectile(GameObject bullet, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(bullet);
        // Now do your thing here
    }



}
