using UnityEngine;

/// <summary>
/// 적이 플레이어를 기다리며 정지해 있는 상태
/// </summary>
public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyController controller) : base(controller) { }

    public override void Enter()
    {
        controller.animator.Play("Idle");
        controller.rb.linearVelocity = Vector2.zero;
    }

    public override void Exit() { }

    public override void Update()
    {
        float distance = Vector2.Distance(controller.transform.position, controller.player.position);
        if (distance < controller.chaseRange)
        {
            controller.TransitionTo(new EnemyChaseState(controller));
        }
    }
}
