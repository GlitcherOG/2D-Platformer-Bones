using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bash : MonoBehaviour
{
    // Script to emulate a bash ability so a player character can 'push/bash' an object out of the way

    //The character that is doing the bashing
    public Rigidbody2D basher;

    // the amount of mass to add to the character
    public float maxMass = 100;

    //the amount of velocity the bash mechanic pushes the character (the mass of the character also needs to be considered in this number)
    public float speed = 40000;

    //the delay it takes before the mass of the character is returned to normal (so it can't push objects anymore)
    private float delay = 0.5f;

    public bool attacking;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //set mass higher to be able to push objects
            basher.mass = maxMass;
            //give the character a boost to 'bash' other rigidbody objects (hardcoded weight and on goes right)
            basher.AddForce(new Vector2(speed, 0));
            //remove the extra mass after the 'delay' time
            StartCoroutine(RemoveMass());
            //attacking set to true;
            attacking = true;
        }
        else
        {
            //attacking set to false;
            attacking = false;
        }
    }


    IEnumerator RemoveMass()
    {
        // Start a delay 
        yield return new WaitForSeconds(delay);
        //change mass to 1 (game standard)
        basher.mass = 1;        
    }
}
