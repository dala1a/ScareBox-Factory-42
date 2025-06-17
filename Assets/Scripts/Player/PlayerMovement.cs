using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

/** 
* Contains the main player behaviors.
* @author: Yunseo Jeon
* @since: 2025-05-25
*/
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CharacterController characterController; // Reference to the player Character Controller. 
    [SerializeField] private float speed = 5f; // The current speed of the player. 
    
    private bool isGrounded; // Check if the player is on the ground
    private Vector3 playerVelocity; // Reference to the player's vel
    private float gravity = -19.6f; // Gravity Constant
    private float jumpForce = 1f; // Jump force Constant
    private float speedMultiplier = 1.5f; // Speed multiplier for sprinting
    private bool isCrouching = false; 
    private float crouchTimer = 1f; // How long it takes to transition in and out of crouching state. 

    /** 
    * Update the player behaviors repeatedly.
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    private void Update()
    {
        isGrounded = characterController.isGrounded; 
        
        // Crouch transitioning behaviors. 
        crouchTimer += Time.deltaTime;
        float p = crouchTimer / 1;
        p *= p;

        if (isCrouching)
            characterController.height = Mathf.Lerp(characterController.height, 1, p);
        else
            characterController.height = Mathf.Lerp(characterController.height, 2, p);

        if (p > 1)
            crouchTimer = 0f;


    }

    /** 
    * Process the player's movement form the input manager. 
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.x = input.x;
        moveDir.z = input.y;
        characterController.Move(transform.TransformDirection(moveDir) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    
    /** 
    * Make the player jump.
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    public void jump()
    {
        if (isGrounded)
            playerVelocity.y = Mathf.Sqrt(jumpForce * -3.0f * gravity);
    }

    
    /** 
    * Make the player sprint.
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    public void sprint(bool isSprinting)
    {
        if (isSprinting)
            speed *= speedMultiplier;
        else
            speed /= speedMultiplier;
    }

    
    /** 
    * Make the player crouch..
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    public void crouch(bool crouching)
    {
        isCrouching = crouching;
        crouchTimer = 0;
    }

}


