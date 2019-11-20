using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractability : MonoBehaviour
{
    public Switch switchTog; //Switch script
    public Bash attack; //If attacking

    private void Update()
    {
        //if there is a script in the variable
        if (switchTog != null)
        {
            //If button Interact is down toggle the switch
            if (Input.GetButtonDown("Interact"))
            {
                switchTog.toggleSwitch();
            }
        }
    }

    //On trigger entered
    private void OnTriggerEnter2D(Collider2D col)
    {
        //If the collided with object that has the tag Item destory it 
        if (col.CompareTag("Item"))
        {
            Destroy(col.gameObject);
        }
        //If the collided with object has the tag switch, Get the componemt switch
        if (col.CompareTag("Switch"))
        {
            switchTog = col.gameObject.GetComponent<Switch>();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //if the collided with object has the tag Switch, Set the variable to null
        if (col.CompareTag("Switch"))
        {
            switchTog = null;
        }
    }
}
