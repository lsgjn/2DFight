using UnityEngine;

public class JumpState : PlayerState
{
    private bool hasJumped = false;
    private float jumpForce = 7f;

    public JumpState(PlayerController controller) : base(controller) {}

    public override void Enter()
    {
        controller.animator.SetState(SpriteAnimator.AnimState.Jump);
        hasJumped = false;
    }

    public override void Exit() {}

    public override void HandleInput() {}

    public override void Update()
    {
        var input = controller.input.InputDirection; 
        if (!hasJumped)
        {
            controller.rb.linearVelocity = new Vector2(controller.rb.linearVelocity.x, jumpForce);  // ✅ 수정
            hasJumped = true;
        }

       
        controller.rb.linearVelocity = new Vector2(input.x * 5f, controller.rb.linearVelocity.y);  // ✅ 수정

        controller.FaceDirection(input.x);

        if (controller.rb.linearVelocity.y <= 0)
            controller.TransitionTo(new FallState(controller));
    }
}
