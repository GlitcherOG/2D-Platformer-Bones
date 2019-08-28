using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController2D controller;
    public float moveSpeed;
    // Start is called before the first frame update
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

        }

        controller.Move(horizontal * moveSpeed);
    }
}
