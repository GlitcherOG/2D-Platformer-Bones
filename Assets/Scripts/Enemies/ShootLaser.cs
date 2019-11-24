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
        //Start a Cornutine for Ienumerator Zap
        StartCoroutine(Zap());
    }
    

    private IEnumerator Zap()
    {
        //Set new GameObject laser to instantiate from the laser prefab
        GameObject laser = Instantiate(laserPrefab);
        //Move the laser to the start position of the laser
        laser.transform.position = startPosition.position;
        //Wait 5 seconds
        yield return new WaitForSeconds(5f);
        //Start a Cornutine for Ienumerator Zap
        StartCoroutine(Zap());
        //Destroy the laster object
        Destroy(laser);
    }
}
