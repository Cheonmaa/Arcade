using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("ESP32 Input Reader")]
    public Esp32InputReader esp32InputReader;

    [Header("Character Controller")]
    public CharacterController2D controller;
    [Header("Animator")]
    public Animator animator;
    [Header("Movement Settings")]
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    // Update is called once per frame
    /*void Update()   //PC version
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (horizontalMove != 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }*/


    void Update()   //esp32 version
    {
        if (CompareTag("Player1"))
        {
            horizontalMove = esp32InputReader.x1 * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            if (esp32InputReader.x1 != 0)
            {
                animator.SetBool("IsWalking", true);
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }
            if (esp32InputReader.y1 == -1)
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }

            if (esp32InputReader.y1 == 1)
            {
                crouch = true;
            }
            else
            {
                crouch = false;
            }
        }
        else if (CompareTag("Player2"))
        {
            horizontalMove = esp32InputReader.x2 * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            if (esp32InputReader.x2 != 0)
            {
                animator.SetBool("IsWalking", true);
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }
            if (esp32InputReader.y2 == -1)
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }

            if (esp32InputReader.y2 == 1)
            {
                crouch = true;
            }
            else
            {
                crouch = false;
            }
        }
        else
        {
            Debug.Log("Player has no tag or tag not detected");
        }
        
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        // Move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
