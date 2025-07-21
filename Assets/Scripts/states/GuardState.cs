using UnityEngine;

public class GuardState : PlayerState
{
    private float guardDuration = 0.6f;
    private float elapsed = 0f;

    // 비활성화 대상 저장
    private Collider2D[] disabledColliders;

    public GuardState(PlayerController controller) : base(controller) {}

    public override void Enter()
    {
        controller.animator.SetState(SpriteAnimator.AnimState.Guard);
        controller.rb.linearVelocity = Vector2.zero;
        elapsed = 0f;

        // ✅ Hitbox 또는 Hurtbox가 붙은 콜라이더만 꺼줌
        var allColliders = controller.GetComponentsInChildren<Collider2D>();
        var tempList = new System.Collections.Generic.List<Collider2D>();

        foreach (var col in allColliders)
        {
            if (col.GetComponent<Hitbox>() || col.GetComponent<Hurtbox>())
            {
                col.enabled = false;
                tempList.Add(col);
            }
        }

        disabledColliders = tempList.ToArray();
        controller.SetGuarding(true);
    }

    public override void Exit()
    {
        // ✅ 꺼준 것만 다시 켜줌
        foreach (var col in disabledColliders)
        {
            if (col != null)
                col.enabled = true;
        }

        controller.SetGuarding(false);
    }

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
