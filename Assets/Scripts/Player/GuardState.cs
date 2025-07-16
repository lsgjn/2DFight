// GuardState.cs
using UnityEngine;

/// <summary>
/// 플레이어가 방어(가드) 중인 상태입니다.
/// 일정 시간 동안 입력 유지 시 유지되며, 방향 전환, 해제 등이 가능합니다.
/// </summary>
public class GuardState : PlayerState
{
    public GuardState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        controller.animator.Play("Guard");

        // 🛠️ 커스텀 포인트:
        // - 가드 이펙트, 무적 프레임, 스태미너 소모 등
        // - AudioManager.Instance.Play("GuardStart");
    }

    public override void Exit()
    {
        // 방어 종료 효과 (소리, 이펙트 등)
    }

    public override void HandleInput()
    {
        // 가드 키를 놓으면 Idle로 복귀
        if (!controller.guardPressed)
        {
            controller.TransitionTo(new IdleState(controller));
            return;
        }

        // 점프 불가 (설정에 따라 가능하게 수정 가능)
        // if (controller.jumpPressed) ...

        // 회피 입력 시 → Dodge로 전환
        if (controller.dodgePressed)
        {
            controller.TransitionTo(new DodgeState(controller));
            return;
        }

        // 🛠️ 커스텀 포인트:
        // - 공격 반격 (패링), 방향 조절, 피격 무시 처리 등 가능
    }

    public override void Update()
    {
        // 캐릭터 방향 유지 (좌우 회전 가능 여부는 게임 디자인에 따라)
        float dir = controller.inputDirection.x;
        if (dir != 0)
            controller.spriteRenderer.flipX = dir < 0;

        // 🛠️ 커스텀 포인트:
        // - 가드 지속 시간, 스태미너 감소
        // - 특정 공격에 대한 피격 무시/경직 계산
    }
}
