using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playersub : MonoBehaviour
{

    private GameObject currentPortal;
    public float portalDistance = 1f;
    public CharacterController2D controller;

    private void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    // Start is called before the first frame update
    private void OnDrawGizmos()
    {
        if (currentPortal != null)
            if (Input.GetKeyDown(KeyCode.E))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(currentPortal.transform.position, portalDistance);
            }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Item"))
        {
            Destroy(col.gameObject);
            GameManager.Instance.AddScore(1);
        }
        if (col.CompareTag("Portal"))
        {
            currentPortal = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Portal"))
        {
            currentPortal = null;
        }
    }
}
