using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitCameraHoriz : MonoBehaviour
{

    
    public float rightLimit;
    public float leftLimit;
    private float startY;

    private void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        if (transform.position.x < leftLimit)
        {
            Debug.Log(transform.position.x + " : " + startY);
            transform.position = transform.position + new Vector3(leftLimit, startY, 0);
                //;
        }


    }
}
