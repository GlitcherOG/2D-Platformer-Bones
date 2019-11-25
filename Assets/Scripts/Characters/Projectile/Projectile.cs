using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectile; //projectile game object
    private bool ready = true; //Bool to test if the arrow is ready to be fired again
    public float projectileSpeed = 5; //Speed that the projectile is fired
    public Animator Anim; //Player Animator
    void Update()
    {
        //If fire button is pressed and ready is true
        if (Input.GetButtonDown("Fire1") && ready)
        {
            //Set ready to false
            ready = false;
            //New Vector3 for would mouse position
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane * 10));
            //Set the Object to look towards the mouse
            transform.LookAt(worldMousePos);
            //Start the coroutine animation
            StartCoroutine(Animation());
        }
    }

    IEnumerator Animation()
    {
        //Set animation trigger attack on
        Anim.SetTrigger("Attack");
        //wait for a few seconds before we destroy the arrow prefab
        yield return new WaitForSeconds(1.20f);
        //Set ready to true
        ready = true;
        //Instantiate a new gameobject
        GameObject arrow = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        //Get the gameobject arrows velocity and transform it to include projectileSpeed
        arrow.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector3(0, 0, projectileSpeed));
        //Start corutine to remove the Projectile
        StartCoroutine(RemoveProjectile(arrow, 5f));
    }


    IEnumerator RemoveProjectile(GameObject bullet, float delayTime)
    {
        //wait for a few seconds before we destroy the arrow prefab
        yield return new WaitForSeconds(delayTime);
        //destroy bullet so we don't build up too many instances of the bullet
        Destroy(bullet);
    }



}