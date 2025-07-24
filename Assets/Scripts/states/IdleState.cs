using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerController controller) : base(controller) {}

    public override void Enter()
    {
        controller.animator.SetState(SpriteAnimator.AnimState.Idle);
        controller.rb.linearVelocity = new Vector2(0, controller.rb.linearVelocity.y);  // ✅ 수정됨
    }

    public override void Exit() {}

    public override void HandleInput()
    {
        var input = controller.input;

        if (Mathf.Abs(input.InputDirection.x) > 0.1f)
            controller.TransitionTo(new RunState(controller));

        else if (input.JumpPressed)
            controller.TransitionTo(new JumpState(controller));

        else if (!controller.IsGrounded())
            controller.TransitionTo(new FallState(controller));

        else if (input.AttackPressed)
            controller.TransitionTo(new AttackState(controller));

        else if (input.GuardPressed)
            controller.TransitionTo(new GuardState(controller));
        
        // else if (input.GuardHeld)
        //     controller.TransitionTo(new ChargingState(controller));
        else if (input.DashPressed)
            controller.TransitionTo(new DashState(controller));

    }

    public override void Update()
    {
        controller.FaceDirection(controller.input.InputDirection.x);
    }
}
