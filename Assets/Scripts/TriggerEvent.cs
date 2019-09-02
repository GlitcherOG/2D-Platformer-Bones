using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent onEnter, OnStay, onExit;
    public UnityEvent onInteract;
    public string hitTag = "Player";

    public void Interact()
    {
        onInteract.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(hitTag))
        {
            onEnter.Invoke();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        onEnter.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onExit.Invoke();
    }
}
