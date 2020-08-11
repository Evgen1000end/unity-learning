using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAnimations : MonoBehaviour
{
    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void SetMovementFloat(float value)
    {
        animator.SetFloat("move", value);
    }

    public float SetCorrectAnimation(float desiredRotationAngle, int angleRotationTreshhold, int inputVerticalDirection)
    {
        float currentAnimationsSpeed = animator.GetFloat("move");
        if (desiredRotationAngle > angleRotationTreshhold || desiredRotationAngle < -angleRotationTreshhold)
        {
            if (Mathf.Abs(currentAnimationsSpeed) < 0.2f)
            {
                currentAnimationsSpeed += inputVerticalDirection * Time.deltaTime * 2;
                currentAnimationsSpeed = Mathf.Clamp(currentAnimationsSpeed, -0.2f, 0.2f);
            }
            SetMovementFloat(currentAnimationsSpeed);
        }
        else
        {
            if (currentAnimationsSpeed < 1)
            {
                currentAnimationsSpeed += inputVerticalDirection * Time.deltaTime * 2;
            }
            SetMovementFloat(Mathf.Clamp(currentAnimationsSpeed, -1, 1));
        }

        return Mathf.Abs(currentAnimationsSpeed);
    }


    public void TriggerLandingAnimation()
    {
        animator.SetTrigger("Land");
    }
    
    public void TriggerJumpAnimation()
    {
        animator.SetTrigger("Jump");
    }
    
    public void TriggerFallAnimation()
    {
        animator.SetTrigger("Fall");
    }
}
