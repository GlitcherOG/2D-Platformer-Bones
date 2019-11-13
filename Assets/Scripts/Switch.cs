using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool toggle; //Switch state bool
    public Sprite[] states; //Diffrent sprite states
    public SpriteRenderer currentState; //current sprite renderer

    private void Start()
    {
        //Get the component sprite renderer
        currentState = GetComponent<SpriteRenderer>();
    }

    public void toggleSwitch(bool toggle = true)
    {
        //If toggle is true change the sprite to last sprite
        if (toggle == true) 
        {
            currentState.sprite = states[states.Length-1];
        }
        else //else change the sprite to the off state
        {
            currentState.sprite = states[0];
        }
    }
}
