// IdleState.cs
using UnityEngine;

/// <summary>
/// 플레이어가 아무 동작도 하지 않는 기본 상태입니다.
/// 입력에 따라 Run, Jump, Guard, Dodge 등의 상태로 전환됩니다.
/// </summary>
public class IdleState : PlayerState
{
    public IdleState(PlayerController controller) : base(controller) { } // 생성자에서 컨트롤러를 주입받습니다.

    public override void Enter()
    {
        controller.animator.Play("Idle");

        // 🛠️ 메커니즘 커스텀 지점:
        // 여기에 Idle 진입 시 효과음, UI 변경 등 추가 가능
        // 예: AudioManager.Instance.Play("IdleBreathe");
    }

    public override void Exit()
    {
        // 상태 종료 시 특별한 작업은 없음 (지속 효과 종료 등 가능)
    }

    public override void HandleInput()
    {
        Vector2 input = controller.inputDirection;

        // → 방향 입력이 있을 경우 Run 상태로 전환
        if (input.x != 0f)
        {
            controller.TransitionTo(new RunState(controller));
            return;
        }

        // 점프 키가 눌렸을 경우
        if (controller.jumpPressed)
        {
            controller.TransitionTo(new JumpState(controller));
            return;
        }

        // 가드 키가 눌렸을 경우
        if (controller.guardPressed)
        {
            controller.TransitionTo(new GuardState(controller));
            return;
        }

        // 회피 키가 눌렸을 경우
        if (controller.dodgePressed)
        {
            controller.TransitionTo(new DodgeState(controller));
            return;
        }

        if (controller.AttackPressed)
        {
            controller.TransitionTo(new AttackState(controller));
            return;
        }
    

        // 🛠️ 메커니즘 커스텀 지점:
        // - 상태 유지 중 아이들 애니메이션 루프 제어
        // - 플레이어 방향 변경, 무기 장비 해제 등 추가 가능
    }

    public override void Update()
    {
        float direction = controller.inputDirection.x;

        if (direction != 0)
            controller.spriteRenderer.flipX = direction > 0;

        // 🛠️ Idle 상태에서 지속적으로 처리할 로직 (예: 캐릭터 회복, 숨쉬기 등)
    }

}
