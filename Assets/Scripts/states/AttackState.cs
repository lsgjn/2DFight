using UnityEngine;

public class AttackState : PlayerState
{
    private float attackDuration = 0.4f;
    private float elapsed = 0f;
    private bool hasActivatedHitbox = false;

    public AttackState(PlayerController controller) : base(controller) {}

    public override void Enter()
{
    controller.animator.SetState(SpriteAnimator.AnimState.Attack); // 캐릭터 애니메이션
    controller.rb.linearVelocity = Vector2.zero;
    elapsed = 0f;
    hasActivatedHitbox = false;

    // ✅ 히트박스 재생 시작 (위치 이동 + 충돌 판정)
    controller.swordHitbox.Activate();
}

    public override void Exit() {}

    public override void HandleInput() {}

    public override void Update()
    {
        var input = controller.input.InputDirection; 
        elapsed += Time.deltaTime;
        
        controller.FaceDirection(input.x);

        if (!hasActivatedHitbox && elapsed >= attackDuration * 0.2f)
        {
            hasActivatedHitbox = true;
            var hitbox = controller.GetComponentInChildren<Hitbox>();
            // hitbox?.Activate();
        }

        if (elapsed >= attackDuration)
            controller.TransitionTo(new IdleState(controller));
    }
}
