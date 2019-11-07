using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractability : MonoBehaviour
{
    public Switch switchTog;
    public CharacterController2D controller;
    public bool attack;

    private void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if (switchTog != null)
        {
            if (Input.GetButtonDown("Interact"))
            {
                switchTog.toggleSwitch();
            }
            if (Input.GetButtonDown("Shift"))
            {
                attack = true;
            }
            if (Input.GetButtonUp("Shift"))
            {
                attack = false;
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
        if (col.CompareTag("Enemy"))
        {

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
