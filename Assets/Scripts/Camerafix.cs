using UnityEngine;

public class Camerafix : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }
}
