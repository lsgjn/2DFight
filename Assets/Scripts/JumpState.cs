// JumpState.cs
using UnityEngine;

/// <summary>
/// 플레이어가 점프하는 상태입니다.
/// 점프 입력 후 상승 중이며, 낙하 전까지 유지됩니다.
/// </summary>
public class JumpState : PlayerState
{
    private bool hasAppliedJump = false;

    public JumpState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        controller.animator.Play("Jump");
        hasAppliedJump = false;

        // 점프 효과음 재생 등 가능
        // 🛠️ 커스텀 포인트:
        // AudioManager.Instance.Play("JumpStart");
    }

    public override void Exit()
    {
        // 상태 종료 시 중력 설정이나 파티클 정리 가능
    }

    public override void HandleInput()
    {
        // 점프 중 추가 입력은 무시하거나,
        // DoubleJumpState 같은 특수 상태로 확장 가능

        // 예: if (Input.GetKeyDown(KeyCode.Space)) => DoubleJumpState

        // 🛠️ 커스텀 포인트:
        // - 점프 중 방향 전환 가능 여부
        // - 스킬 사용 가능 여부 제어
    }

    public override void Update()
    {
        // 점프력 적용 (딜레이 방지)
        if (!hasAppliedJump)
        {
            controller.rb.linearVelocity = new Vector2(controller.rb.linearVelocity.x, controller.jumpForce);
            hasAppliedJump = true;
        }

        // 방향 입력에 따라 공중 이동
        float direction = controller.inputDirection.x;
        controller.rb.linearVelocity = new Vector2(direction * controller.moveSpeed, controller.rb.linearVelocity.y);

        // 캐릭터 방향 반영
        if (direction != 0)
            controller.spriteRenderer.flipX = direction > 0;

        // 낙하 상태로 전이 (y 속도가 0보다 작아지는 시점)
        if (controller.rb.linearVelocity.y <= 0)
        {
            controller.TransitionTo(new FallState(controller));
        }

        // 🛠️ 커스텀 포인트:
        // - 공중 회전 애니메이션
        // - 점프 중 공격/회피 허용 여부
        // - 점프 딜레이 or 스태미너 소모 등
    }
}
