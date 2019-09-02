using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public CharacterController2D[] controller;
    public CinemachineVirtualCamera Camera;
    public Text characternames;
    public int character;
    public float[] moveSpeed;
    public float[] jumpHeight;
    public float[] climbSpeed;
    public string[] names;
    public float portalDistance = 1f;

    // Start is called before the first frame update
    private GameObject currentPortal;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool isJumping = Input.GetButtonDown("Jump");

        if (isJumping)
        {
            controller[character].Jump(jumpHeight[character]);
        }

        controller[character].Climb(vertical * climbSpeed[character]);
        controller[character].Move(horizontal * moveSpeed[character]);

        if (currentPortal != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentPortal.SendMessage("Interact");
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (character >= controller.Length - 1)
            {
                character = 0;
            }
            else
            {
                character++;
            }
            characternames.text = names[character];
            Camera.Follow = controller[character].gameObject.transform;
        }
    }
}
