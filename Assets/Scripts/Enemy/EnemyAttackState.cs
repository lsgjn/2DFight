using UnityEngine;

/// <summary>
/// 적이 공격하는 상태 (단순한 애니메이션 실행 + 타이머)
/// </summary>
public class EnemyAttackState : EnemyState
{
    private float attackCooldown = 1.5f;
    private float elapsed = 0f;

    public EnemyAttackState(EnemyController controller) : base(controller) { }

    public override void Enter()
    {
        controller.animator.Play("Attack");
        elapsed = 0f;
        controller.rb.linearVelocity = Vector2.zero;
    }

    public override void Exit() { }

    public override void Update()
    {
        elapsed += Time.deltaTime;

        float distance = Vector2.Distance(controller.transform.position, controller.player.position);
        if (elapsed >= attackCooldown)
        {
            if (distance > controller.attackRange)
                controller.TransitionTo(new EnemyChaseState(controller));
            else
                controller.TransitionTo(new EnemyAttackState(controller)); // 반복 공격
        }
    }
}
