using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bash : MonoBehaviour
{
    public Rigidbody2D basher;
    private float delay;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            basher.mass = 500;
            basher.AddForce(new Vector2(100, 0));
        }

    }
}
