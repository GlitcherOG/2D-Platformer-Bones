using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent onEnter, OnStay, onExit; //Unity events for entering, staying and exiting 
    public UnityEvent onInteract; //Unity event for when interacting with an object;
    public string hitTag = "Player"; //hitTag for checking that the player has triggered the event

    public void Interact()
    {
        //When void in run invoke all callbacks on interact
        onInteract.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //If player enters the trigger invoke all callbacks onEnter
        if (col.CompareTag(hitTag))
        {
            onEnter.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //If player stays within the trigger invoke all callbacks onStay
        if (collision.CompareTag(hitTag))
        {
            OnStay.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //On trigger exit, Invoke All Callbacks on onExit
        onExit.Invoke();
    }
}
