using UnityEngine;

/// <summary>
/// 대시 상태 - 일정 시간 고속 이동 후 Idle로 전환됨.
/// 이동 중 투명도 조절 및 방향 고정
/// </summary>
public class DashState : PlayerState
{
    private float dashDuration = 0.4f; // 대시 시간 증가
    private float elapsed = 0f;
    private Vector2 dashDirection;
    private Color originalColor;

    public DashState(PlayerController controller) : base(controller) {}

    public override void Enter()
    {
        SoundManager.Instance.PlayDash();

        controller.animator.SetState(SpriteAnimator.AnimState.Dash);
        elapsed = 0f;

        // 방향 설정
        float inputX = controller.input.InputDirection.x;
        dashDirection = inputX != 0f 
            ? new Vector2(inputX, 0f).normalized 
            : (controller.IsFacingRight ? Vector2.right : Vector2.left);

        controller.FaceDirection(dashDirection.x);

        // 투명도 조절 (알파 0.8f)
        if (controller.spriteRenderer != null)
        {
            originalColor = controller.spriteRenderer.color;
            Color faded = originalColor;
            faded.a = 0.8f;
            controller.spriteRenderer.color = faded;
        }
    }

    public override void Exit()
    {
        // 속도 정지
        controller.rb.linearVelocity = new Vector2(0f, controller.rb.linearVelocity.y);

        // 원래 투명도로 복원
        if (controller.spriteRenderer != null)
            controller.spriteRenderer.color = originalColor;
    }

    public override void HandleInput()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= dashDuration)
        {
            controller.TransitionTo(new IdleState(controller));
            return;
        }

        // 대시 이동 (기존보다 빠르게)
        float dashSpeed = controller.dodgeSpeed * 5f;
        controller.rb.linearVelocity = new Vector2(dashDirection.x * dashSpeed, controller.rb.linearVelocity.y);
    }

    public override void Update()
    {
        // 필요 시 시각 효과 추가 가능
    }
}
