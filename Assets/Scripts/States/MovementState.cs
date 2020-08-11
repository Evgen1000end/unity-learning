using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : BaseState
{
    float fallingDelay = 0;
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        fallingDelay = 0.2f;
    }

    public override void HandleMovement(Vector2 input)
    {
        base.HandleMovement(input);
        controllerReference.movement.HandleMovement(input);
        
    }

    public override void HandleCameraDirection(Vector3 input)
    {
        base.HandleCameraDirection(input);
        controllerReference.movement.HandleMovementDirection(input);
    }

    public override void HandleJumpInput()
    {
        controllerReference.TransitionToState(controllerReference.jumpState);
    }

    private long frameCount = 0;
    public override void Update()
    {
        frameCount++;
        base.Update();
        HandleMovement(controllerReference.input.MovementInputVector);
        HandleCameraDirection(controllerReference.input.MovementDirectionVector);
        if(controllerReference.movement.IsGround() == false)
        {
            Debug.Log("Why am I not on the ground? " + frameCount);
            if(fallingDelay > 0)
            {
                fallingDelay -= Time.deltaTime;
                return;
            }
            Debug.Log("I'm falling! " + frameCount);
            controllerReference.TransitionToState(controllerReference.fallingState);
        }
        else
        {
            Debug.Log("I can feel the ground under my feet! " + frameCount);    
        }
    }
}
