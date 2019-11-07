using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    //Prefeab of laser asset
    public Transform laserPrefab;
    //position to instanciate laser
    public Transform startPosition;

    private void Start()
    {
        StartCoroutine(Zap());
    }
    

    private IEnumerator Zap()
    {
        yield return new WaitForSeconds(5f);
        //new GameObject("Laser").AddComponent<Fractal>().Initialize(this, i);
        Transform laser = Instantiate(laserPrefab);
        laser.position = startPosition.position;
        StartCoroutine(Zap());
    }
}
