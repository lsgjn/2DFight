using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 플레이어의 방어 상태 - 일정 시간 동안 히트박스/헐트박스 비활성화, 가드 판정 활성화
/// </summary>
public class GuardState : PlayerState
{
    private float guardDuration = 0.6f;
    private float elapsed = 0f;

    private Collider2D[] disabledColliders;

    public GuardState(PlayerController controller) : base(controller) {}

    public override void Enter()
    {
        if (elapsed == 0f) // 처음 진입 시에만 사운드 재생
        SoundManager.Instance.PlayGuard();
        controller.animator.SetState(SpriteAnimator.AnimState.Guard);
        controller.rb.linearVelocity = Vector2.zero;
        elapsed = 0f;

        // ✅ 히트박스, 헐트박스만 비활성화
        var allColliders = controller.GetComponentsInChildren<Collider2D>();
        var list = new List<Collider2D>();

        foreach (var col in allColliders)
        {
            if (col.GetComponent<Hitbox>())
            {
                col.enabled = false;
                list.Add(col); // ✅ 빠졌던 부분 추가
            }
        }

        disabledColliders = list.ToArray();

        // ✅ 가드박스는 활성화
        // if (controller.guardBox != null)
        //     controller.guardBox.SetActive(true);

        // var parry = controller.GetComponent<ParrySystem>();
        // parry?.ActivateParry();

        controller.SetGuarding(true);
    }

    public override void Exit()
    {
        // ✅ 방어 상태에서 비활성화했던 것만 다시 복구
        if (disabledColliders != null)
        {
            foreach (var col in disabledColliders)
            {
                if (col != null && col.GetComponent<Hitbox>())
                    col.enabled = true;
            }
        }

        // // ✅ 가드박스는 다시 비활성화
        // if (controller.guardBox != null)
        //     controller.guardBox.SetActive(false);

        controller.SetGuarding(false);
    }

    public override void HandleInput()
    {
        if (!controller.input.GuardPressed)
            controller.TransitionTo(new IdleState(controller));
    }

    public override void Update()
    {
        elapsed += Time.deltaTime;
        controller.FaceDirection(controller.input.InputDirection.x);

        if (elapsed >= guardDuration)
            controller.TransitionTo(new IdleState(controller));
    }
}
