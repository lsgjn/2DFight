using UnityEngine;
public class AttackState : PlayerState
{
    private float attackDuration = 0.4f;
    private float elapsed = 0f;
    private bool hasActivatedHitbox = false;

    public AttackState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        controller.animator.SetState(SpriteAnimator.AnimState.Attack);
        controller.rb.linearVelocity = Vector2.zero;
        elapsed = 0f;
        hasActivatedHitbox = false;
    }

    public override void Update()
    {
        elapsed += Time.deltaTime;

        controller.FaceDirection(controller.input.InputDirection.x);

        if (!hasActivatedHitbox && elapsed >= attackDuration * 0.2f)
        {
            hasActivatedHitbox = true;
            controller.swordHitbox.Activate();
        }

        if (elapsed >= attackDuration)
        {
            controller.TransitionTo(new IdleState(controller));
        }
        
    }

    public override void HandleInput() { }
    public override void Exit() { }
}
