using UnityEngine;

public class ChargedAttackState : PlayerState
{
    private float attackDuration = 0.5f;
    private float elapsed = 0f;
    private bool hasActivatedHitbox = false;

    public ChargedAttackState(PlayerController controller) : base(controller) {}

    public override void Enter()
    {
        controller.rb.linearVelocity = Vector2.zero;

        // 🔥 특수공격 애니메이션 재생
        controller.animator.SetState(SpriteAnimator.AnimState.ChargedAttack);  
        elapsed = 0f;
        hasActivatedHitbox = false;

        Debug.Log("⚡ 특수 공격 시작!");
    }

    public override void Exit() {}

    public override void HandleInput() {}

    public override void Update()
    {
        elapsed += Time.deltaTime;

        // 일정 시간 후 히트박스 발동
        if (!hasActivatedHitbox && elapsed >= 0.15f)
        {
            // controller.swordHitbox.Activate(isCharged: true); // 강한 히트박스 활성화
            hasActivatedHitbox = true;
        }

        // 공격이 끝나면 Idle로 전환
        if (elapsed >= attackDuration)
        {
            controller.TransitionTo(new IdleState(controller));
        }
    }
}

