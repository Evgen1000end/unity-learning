using UnityEngine;


    public class JumpState: BaseState
    {
        private bool landingTrigger = false;
        private float delay = 0f;
        
        public override void EnterState(AgentController controller)
        {
            base.EnterState(controller);
            delay = 0.2f;
            base.EnterState(controller);
            landingTrigger = false;
            controllerReference.movement.HandleJump();
        }


        public override void Update()
        {
            base.Update();

            if (delay > 0)
            {
                delay -= Time.deltaTime;
                return;
            }
            
            if (controllerReference.movement.IsGround())
            {
               
                if (landingTrigger == false)
                {
                    landingTrigger = true;
                    controllerReference.agentAnimations.TriggerLandingAnimation();
                }

                if (controllerReference.movement.HasFinishedJumping())
                {
                    controllerReference.TransitionToState(controllerReference.movementState);
                }
            }
       
        }


    }
    
    
    
