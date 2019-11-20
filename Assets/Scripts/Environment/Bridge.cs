using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bridge : MonoBehaviour
{
    public Switch tog;
    public UnityEvent onToggle;

    void Update()
    {
        if(tog.toggle == true)
        {
            onToggle.Invoke();
        }
    }
}
