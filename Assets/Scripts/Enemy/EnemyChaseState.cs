using UnityEngine;

/// <summary>
/// 플레이어를 향해 이동하는 상태
/// </summary>
public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(EnemyController controller) : base(controller) { }

    public override void Enter()
    {
        controller.animator.Play("Run");
    }

    public override void Exit()
    {
        controller.rb.linearVelocity = Vector2.zero;
    }

    public override void Update()
    {
        Transform player = controller.player;
        Vector2 dir = (player.position - controller.transform.position).normalized;
        controller.rb.linearVelocity = new Vector2(dir.x * controller.moveSpeed, controller.rb.linearVelocity.y);

        // 시선 방향 맞추기
        if (dir.x != 0)
            controller.spriteRenderer.flipX = dir.x < 0;

        float distance = Vector2.Distance(controller.transform.position, player.position);
        if (distance < controller.attackRange)
        {
            controller.TransitionTo(new EnemyAttackState(controller));
        }
        else if (distance > controller.chaseRange + 2f)
        {
            controller.TransitionTo(new EnemyIdleState(controller));
        }
    }
}
