using UnityEngine;

namespace States
{
    public class MovementState: BaseState
    {
        private float fallingDelay;
        
        public override void EnterState(AgentController controller)
        {
            base.EnterState(controller);
            fallingDelay = 0.2f;
        }

        public override void HandleMovement(Vector2 input)
        {
            base.HandleMovement(input);
            controllerReference.movement.HandleMovement(controllerReference.input.MovementInputVector);
        }

        public override void HandleCameraDirection(Vector3 input)
        {
            base.HandleCameraDirection(input);
            controllerReference.movement.HandleMovementDirection(controllerReference.input.MovementDirectionVector);
        }

        public override void HandleJumpInput()
        {
            base.HandleJumpInput();
            controllerReference.TransitionToState(controllerReference.jumpState);
        }

        public int count = 0;

        public override void Update()
        {
            count++;
            base.Update();
            HandleMovement(controllerReference.input.MovementInputVector);
            HandleCameraDirection(controllerReference.input.MovementDirectionVector);

            if (controllerReference.movement.IsGround() == false)
            {
                if (fallingDelay > 0)
                {
                    fallingDelay -= Time.deltaTime;
                    return;
                }
                Debug.Log("Я не на земле! и перехожу в падение!" + count);
                controllerReference.TransitionToState(controllerReference.fallingState);
            }
        }
    }
}