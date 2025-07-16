// DodgeState.cs
using UnityEngine;

/// <summary>
/// 플레이어가 회피 동작 중인 상태입니다.
/// 짧은 시간 동안 무적 상태를 적용하고, 이동 후 Idle로 전환됩니다.
/// </summary>
public class DodgeState : PlayerState
{
    private float dodgeDuration = 0.3f; // 회피 시간
    private float elapsed = 0f;
    private float dodgeSpeed = 10f;
    private int dodgeDirection = 1;

    public DodgeState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        controller.animator.Play("Dodge");
        elapsed = 0f;

        // 방향 저장 (입력 없으면 현재 방향 유지)
        dodgeDirection = (int)Mathf.Sign(controller.inputDirection.x != 0 ? controller.inputDirection.x : (controller.spriteRenderer.flipX ? -1 : 1));

        // 🛠️ 커스텀 포인트:
        // - 무적 상태 부여
        // - 스태미너 소비
        // - AudioManager.Instance.Play("DodgeRoll");
    }

    public override void Exit()
    {
        // 무적 상태 해제 등 처리 가능
    }

    public override void HandleInput()
    {
        // 회피 중에는 입력 무시 (또는 방향 조절 가능하게 커스터마이즈)
    }

    public override void Update()
    {
        elapsed += Time.deltaTime;

        // 이동 적용 (짧은 시간 동안 빠르게 이동)
        controller.rb.velocity = new Vector2(dodgeDirection * dodgeSpeed, controller.rb.velocity.y);

        if (elapsed >= dodgeDuration)
        {
            controller.TransitionTo(new IdleState(controller));
        }

        // 🛠️ 커스텀 포인트:
        // - 회피 후 경직
        // - 방향 조절 가능 여부
        // - 공격을 무시하는 시간 설정 등
    }
}
