namespace States
{
    public class FallingState : JumpState
    {
        public override void EnterState(AgentController controller)
        {
            base.EnterState(controller);
            controllerReference.agentAnimations.TriggerFallAnimation();
            controllerReference.movement.SetFinishJumpingFalse();
        }

        public override void Update()
        {
            base.Update();
            
        }
    }
    
}