using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    //Prefeab of laser asset
    public GameObject laserPrefab;
    //position to instanciate laser
    public Transform startPosition;

    private void Start()
    {
        StartCoroutine(Zap());
    }
    

    private IEnumerator Zap()
    {
        //new GameObject("Laser").AddComponent<Fractal>().Initialize(this, i);
        GameObject laser = Instantiate(laserPrefab);
        laser.transform.position = startPosition.position;
        yield return new WaitForSeconds(5f);
        StartCoroutine(Zap());
        Destroy(laser);

    }
}
