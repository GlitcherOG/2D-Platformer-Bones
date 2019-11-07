using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    
    // A laser prefab that shoots directly down (is visual only)
    void Start()
    {
        //move the component to the forground as it's a background visual object and does not interact
        this.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";        
    }

    
    void Update()
    {
        // move the laser down
        transform.Translate((Vector3.down * Time.deltaTime) * 4);
        //(the prefab is destroyed after a few seconds in the shoot script)
    }

}
