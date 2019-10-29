using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    public GameObject projectile;

    public float projectileSpeed = 5;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane * 10));

            transform.LookAt(worldMousePos);
            GameObject bullet = Instantiate(projectile, transform.position, transform.rotation) as GameObject;

            bullet.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector3(0, 0, projectileSpeed));
            StartCoroutine(RemoveProjectile(bullet, 5f));
        }
    }


    IEnumerator RemoveProjectile(GameObject bullet, float delayTime)
    {
        //wait for a few seconds before we destroy the arrow prefab
        yield return new WaitForSeconds(delayTime);
        //destroy bullet so we don't build up too many instances of the bullet
        Destroy(bullet);
    }



}