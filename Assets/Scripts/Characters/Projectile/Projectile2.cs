using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    public GameObject projectile;
    public bool ready = true;
    public float projectileSpeed = 5;
    public Animator Anim;
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && ready)
        {
            ready = false;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane * 10));

            transform.LookAt(worldMousePos);
            Anim.SetTrigger("Attack");

            StartCoroutine(Animation());
        }
    }

    IEnumerator Animation()
    {
        Anim.SetTrigger("Attack");
        //wait for a few seconds before we destroy the arrow prefab
        yield return new WaitForSeconds(1.20f);
        ready = true;
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector3(0, 0, projectileSpeed));
        StartCoroutine(RemoveProjectile(bullet, 5f));
    }


    IEnumerator RemoveProjectile(GameObject bullet, float delayTime)
    {
        //wait for a few seconds before we destroy the arrow prefab
        yield return new WaitForSeconds(delayTime);
        //destroy bullet so we don't build up too many instances of the bullet
        Destroy(bullet);
    }



}