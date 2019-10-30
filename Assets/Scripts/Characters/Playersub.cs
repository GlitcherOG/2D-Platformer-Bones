using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playersub : MonoBehaviour
{

    private GameObject currentPortal;
    public Switch switchTog;
    public float portalDistance = 1f;
    public CharacterController2D controller;

    private void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if (switchTog != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                switchTog.toggleSwitch();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Item"))
        {
            Destroy(col.gameObject);
        }
        if (col.CompareTag("Switch"))
        {
            switchTog = col.gameObject.GetComponent<Switch>();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Switch"))
        {
            switchTog = null;
        }
    }
}
