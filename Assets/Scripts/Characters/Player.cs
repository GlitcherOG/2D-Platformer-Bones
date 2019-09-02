using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController2D controller;

    public float moveSpeed = 10f;
    public float jumpHeight = 5f;
    public float climbSpeed = 10f;
    public float portalDistance = 1f;
    // Start is called before the first frame update

    private GameObject currentPortal;
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool isJumping = Input.GetButtonDown("Jump");

        if (isJumping)
        {
            controller.Jump(jumpHeight);
        }
        controller.Climb(vertical * climbSpeed);
        controller.Move(horizontal * moveSpeed);

        if (currentPortal != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentPortal.SendMessage("Interact");
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(currentPortal != null)
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
