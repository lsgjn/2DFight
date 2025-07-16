using UnityEngine;

public class AttackState : PlayerState
{
    private float attackDuration = 0.4f;
    private float elapsed = 0f;
    private bool hasAppliedHit = false;

    public AttackState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        base.Enter();
        controller.Animator.Play("Attack");
        controller.Rigidbody.velocity = Vector2.zero;
        hasAppliedHit = false;
        elapsed = 0f;
        Debug.Log("Attack 상태 진입");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        elapsed += Time.deltaTime;

        if (!hasAppliedHit && elapsed >= attackDuration * 0.2f)
        {
            hasAppliedHit = true;
            ApplyAttackHitbox();
        }

        if (elapsed >= attackDuration)
        {
            controller.TransitionTo(new IdleState(controller));
        }
    }

    private void ApplyAttackHitbox()
    {
        Debug.Log("일반 공격 히트 체크");

        Hitbox hitbox = controller.GetComponentInChildren<Hitbox>();
        if (hitbox != null)
        {
            hitbox.Activate();
        }
        else
        {
            Debug.LogWarning("Hitbox 컴포넌트를 찾을 수 없습니다.");
        }
    }
} 
