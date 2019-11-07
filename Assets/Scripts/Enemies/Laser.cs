using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.down * Time.deltaTime) * 4);
    }
}
