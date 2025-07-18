using UnityEngine;

public class RunState : PlayerState
{
    private float moveSpeed = 5f;

    public RunState(PlayerController controller) : base(controller) {}

    public override void Enter()
    {
        controller.animator.SetState(SpriteAnimator.AnimState.Run);
    }

    public override void Exit() {}

    public override void HandleInput()
    {
        var input = controller.input;

        if (Mathf.Abs(input.InputDirection.x) < 0.1f)
            controller.TransitionTo(new IdleState(controller));

        else if (input.JumpPressed)
            controller.TransitionTo(new JumpState(controller));

        else if (!controller.IsGrounded())
            controller.TransitionTo(new FallState(controller));

        else if (input.AttackPressed)
            controller.TransitionTo(new AttackState(controller));

        else if (input.GuardPressed)
            controller.TransitionTo(new GuardState(controller));
    }

    public override void Update()
    {
        Vector2 input = controller.input.InputDirection;

        // ✅ 수정된 부분
        controller.rb.linearVelocity = new Vector2(input.x * moveSpeed, controller.rb.linearVelocity.y);

        controller.FaceDirection(controller.input.InputDirection.x);
    }
}
