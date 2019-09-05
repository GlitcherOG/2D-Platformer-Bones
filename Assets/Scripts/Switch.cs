using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool toggle;
    public bool oneTime;
    public Sprite[] states;
    public SpriteRenderer currentState;
    // Start is called before the first frame update/
    private void Start()
    {
        currentState = GetComponent<SpriteRenderer>();
    }
    public void toggleSwitch()
    {
        if (oneTime == false)
        {
            toggle = !toggle;
        }
        else if (toggle == false)
        {
            toggle = true;
        }
        if (toggle == true)
        {
            currentState.sprite = states[1];
        }
        else
        {
            currentState.sprite = states[0];
        }
    }

}
