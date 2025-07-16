// FallState.cs
using UnityEngine;

/// <summary>
/// 플레이어가 낙하 중인 상태입니다.
/// y 속도가 0보다 작을 때 진입하며, 바닥에 닿으면 Idle 상태로 전환됩니다.
/// </summary>
public class FallState : PlayerState
{
    public FallState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        controller.animator.Play("Fall");

        // 🛠️ 커스텀 포인트:
        // - 낙하 중 효과음, 연기 파티클
        // - 긴 낙하 시 착지 충격량 계산 등도 가능
    }

    public override void Exit()
    {
        // 낙하 종료 효과나 파티클 정리 가능
    }

    public override void HandleInput()
    {
        // 낙하 중 회피 가능
        if (controller.dodgePressed)
        {
            controller.TransitionTo(new DodgeState(controller));
            return;
        }

        // 낙하 중 가드 가능 (설정에 따라 제한할 수 있음)
        // if (controller.guardPressed)
        // {
        //     controller.TransitionTo(new GuardState(controller));
        //     return;
        // }

        // 🛠️ 커스텀 포인트:
        // - 공중 공격, 공중 대시 등 처리 가능
    }

    public override void Update()
    {
        float direction = controller.inputDirection.x;

        // 공중에서 좌우 이동
        controller.rb.linearVelocity = new Vector2(direction * controller.moveSpeed, controller.rb.linearVelocity.y);

        // 캐릭터 방향 전환
        if (direction != 0)
            controller.spriteRenderer.flipX = direction < 0;

        // 착지 판별
        if (IsGrounded())
        {
            controller.TransitionTo(new IdleState(controller));
        }

        // 🛠️ 커스텀 포인트:
        // - 착지 강도 체크 → 경직 상태 진입
        // - 플랫폼 통과 처리 (아래 방향키 + 점프 등은 점프에서 처리 예정)
    }

    /// <summary>
    /// 간단한 착지 판정: 바닥에 닿았는지 확인
    /// </summary>
    private bool IsGrounded()
    {
        // Collider2D col = controller.GetComponent<Collider2D>();
        // Vector2 origin = (Vector2)col.bounds.center + Vector2.down * (col.bounds.extents.y + 0.05f);
        // float rayLength = 0.2f;
        // RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayLength, LayerMask.GetMask("Ground"));
        // Debug.DrawRay(origin, Vector2.down * rayLength, Color.red, 0.1f);
        // return hit.collider != null;
        return controller.rb.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}
