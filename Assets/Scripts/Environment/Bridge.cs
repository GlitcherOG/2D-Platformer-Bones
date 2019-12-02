using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bridge : MonoBehaviour
{
    public Switch tog; //Switch Toggle Script
    public UnityEvent onToggle; //Unity event for when toggle is true

    void Update()
    {
        //If the Switch is on invoke Ontoggle
        if(tog.toggleState == true)
        {
            onToggle.Invoke();
        }
    }
}
