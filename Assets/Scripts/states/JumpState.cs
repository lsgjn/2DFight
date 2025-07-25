using UnityEngine;

public class JumpState : PlayerState
{
    private bool hasJumped = false;
    private float elapsed = 0f;

    public JumpState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        if (elapsed == 0f) // 처음 진입 시에만 사운드 재생
            SoundManager.Instance.PlayJump();
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
            // ✅ 프리팹 설정값 기반 점프력 사용
            controller.rb.linearVelocity = new Vector2(controller.rb.linearVelocity.x, controller.jumpForce);
            hasJumped = true;
        }

        // ✅ 프리팹 설정값 기반 수평 속도 사용
        controller.rb.linearVelocity = new Vector2(input.x * controller.moveSpeed, controller.rb.linearVelocity.y);

        controller.FaceDirection(input.x);

        if (controller.rb.linearVelocity.y <= 0)
            controller.TransitionTo(new FallState(controller));
    }
}
