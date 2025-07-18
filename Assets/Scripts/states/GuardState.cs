using UnityEngine;

public class GuardState : PlayerState
{
    private float guardDuration = 0.6f;
    private float elapsed = 0f;

    public GuardState(PlayerController controller) : base(controller) {}

    public override void Enter()
    {
        controller.animator.SetState(SpriteAnimator.AnimState.Guard);
        controller.rb.linearVelocity = Vector2.zero;  // ✅ 수정됨
        elapsed = 0f;
    }

    public override void Exit() {}

    public override void HandleInput()
    {
        if (!controller.input.GuardPressed)
            controller.TransitionTo(new IdleState(controller));
    }

    public override void Update()
    {
        var input = controller.input.InputDirection; 
        elapsed += Time.deltaTime;
        controller.FaceDirection(input.x);
        if (elapsed >= guardDuration)
            controller.TransitionTo(new IdleState(controller));
    }
}
