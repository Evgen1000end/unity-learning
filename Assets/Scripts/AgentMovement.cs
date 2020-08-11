using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    protected CharacterController characterController;
    public float movementSpeed;
    public float gravity;
    public float rotationSpeed;
    protected Vector3 moveDirection = Vector3.zero;
    protected float desiredRotationAngle = 0;
    protected int inputVerticalDirection = 0;
    protected HumanoidAnimations agentAnimations;

    public int angleRotationTreshhold;

    private bool isJumping = false;
    private bool finishJumping = true;
    
    public float jumpSpeed;

    public void HandleMovement(Vector2 input)
    {
        if (characterController.isGrounded)
        {
            if (input.y != 0)
            {
                if (input.y > 0)
                {
                    inputVerticalDirection = Mathf.CeilToInt(input.y);
                }
                else
                {
                    inputVerticalDirection = Mathf.FloorToInt(input.y);
                }
                moveDirection = input.y * transform.forward * movementSpeed;
            }
            else
            {
                moveDirection = Vector3.zero;
                agentAnimations.SetMovementFloat(0);
            }
        }
    }

    public void HandleJump()
    {
        if (characterController.isGrounded)
        {
            isJumping = true;
            Debug.Log("isJumping = true");
        }
    }


    public void HandleMovementDirection(Vector3 input)
    {
        desiredRotationAngle = Vector3.Angle(transform.forward, input);
        var crossProduct = Vector3.Cross(transform.forward, input).y;
        if (crossProduct < 0)
        {
            desiredRotationAngle *= -1;
        }
        
    }

    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        agentAnimations = GetComponent<HumanoidAnimations>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            if (moveDirection.magnitude > 0)
            {
                var animationSpeedMultiplier = agentAnimations.SetCorrectAnimation(desiredRotationAngle, angleRotationTreshhold, inputVerticalDirection);
                RotateAgent();
                moveDirection *= animationSpeedMultiplier;
                
            }
        }
        
        moveDirection.y -= gravity;

        if (isJumping)
        {
            isJumping = false;
            finishJumping = false;
            
            moveDirection.y = jumpSpeed;
            agentAnimations.SetMovementFloat(0);
            agentAnimations.TriggerJumpAnimation();
            Debug.Log("Jump physics apply");
        }
        
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void RotateAgent()
    {
        if (desiredRotationAngle > angleRotationTreshhold || desiredRotationAngle < - angleRotationTreshhold)
        {
            transform.Rotate(Vector3.up * desiredRotationAngle * rotationSpeed * Time.deltaTime);
        }
    }

    public void StopMovementImmediatelly()
    {
        moveDirection = Vector3.zero;
        finishJumping = false;

    }

    public bool HasFinishedJumping()
    {
        return finishJumping;
    }

    public void SetFinishJumping()
    {
        finishJumping = true;
    }
    
    public void SetFinishJumpingTrue()
    {
        finishJumping = true;
    }
    
    public void SetFinishJumpingFalse()
    {
        finishJumping = false;
    }


    public bool IsGround()
    {
        return characterController.isGrounded;
    }
}
