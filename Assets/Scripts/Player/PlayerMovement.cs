using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
    public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CharacterController characterController; 
    [SerializeField] private float speed = 5f; 

    private bool isGrounded;
    private Vector3 playerVelocity; 
    private float gravity = -19.6f; 
    private float jumpForce = 1f; 
    private float speedMultiplier = 1.5f; 
    private bool isCrouching = false;
    private float crouchTimer = 1f; 
    
    private void Update()
    { 
        isGrounded = characterController.isGrounded;

        crouchTimer += Time.deltaTime; 
        float p = crouchTimer / 1; 
        p *= p; 

        if(isCrouching)
            characterController.height = Mathf.Lerp(characterController.height, 1, p); 
        else 
            characterController.height = Mathf.Lerp(characterController.height, 2, p); 

        if(p > 1)
            crouchTimer = 0f;
    

    }

    public void ProcessMove(Vector2 input)
    { 
        Vector3 moveDir = Vector3.zero; 
        moveDir.x = input.x; 
        moveDir.z = input.y;
        characterController.Move(transform.TransformDirection(moveDir) * speed * Time.deltaTime);  
        playerVelocity.y += gravity * Time.deltaTime; 
        if(isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f; 
        characterController.Move(playerVelocity * Time.deltaTime); 
    }

    public void jump()
    { 
        if(isGrounded)
            playerVelocity.y = Mathf.Sqrt(jumpForce * -3.0f * gravity); 
    }

    public void sprint(bool isSprinting)
    { 
        if(isSprinting)
            speed *= speedMultiplier;
        else 
            speed /= speedMultiplier;   
    }

    public void crouch(bool crouching)
    {   
        isCrouching = crouching; 
        crouchTimer = 0; 
    }

}


