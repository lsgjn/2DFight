using UnityEngine;

public class FallState : PlayerState
{
    public FallState(PlayerController controller) : base(controller) {}

    public override void Enter()
    {
        controller.animator.SetState(SpriteAnimator.AnimState.Fall);
    }

    public override void Exit() {}

    public override void HandleInput()
    {
        if (controller.input.GuardPressed)
            controller.TransitionTo(new GuardState(controller));

        else if (controller.input.AttackPressed)
            controller.TransitionTo(new AttackState(controller));
    }

    public override void Update()
    {
        var input = controller.input.InputDirection;
        controller.rb.linearVelocity = new Vector2(input.x * 5f, controller.rb.linearVelocity.y);  // ✅ 수정됨
        controller.FaceDirection(input.x);

        if (controller.IsGrounded())
            controller.TransitionTo(new IdleState(controller));
    }
}
