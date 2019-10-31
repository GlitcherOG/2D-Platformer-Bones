using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool toggle;
    public Sprite[] states;
    public SpriteRenderer currentState;
    // Start is called before the first frame update/
    private void Start()
    {
        currentState = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (toggle == true)
        {
            currentState.sprite = states[states.Length - 1];
        }
        else
        {
            currentState.sprite = states[0];
        }
    }

    public void toggleSwitch()
    {
        toggle = true;
        if (toggle == true)
        {
            currentState.sprite = states[states.Length-1];
        }
        else
        {
            currentState.sprite = states[0];
        }
    }
}
