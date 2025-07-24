using UnityEngine;

public class AttackState : PlayerState
{
    private float attackDuration = 0.4f;
    private float elapsed = 0f;
    private bool hasActivatedHitbox = false;

    private float startupTime = 0.1f;       // 전딜: 약 1.3프레임
    //private float parryableTime = 0.15f;    // 패링 가능 시간 유지: 약 2프레임

    public AttackState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        controller.animator.SetState(SpriteAnimator.AnimState.Attack);
        controller.rb.linearVelocity = Vector2.zero;
        elapsed = 0f;
        hasActivatedHitbox = false;
        controller.IsParryable = false;
        SoundManager.Instance.PlayNormalsword();
    }

    public override void Update()
    {
        elapsed += Time.deltaTime;
        controller.FaceDirection(controller.input.InputDirection.x);

        // 공격 시작 타이밍
        if (!hasActivatedHitbox && elapsed >= startupTime)
        {
            hasActivatedHitbox = true;
            controller.swordHitbox.Activate();
            // controller.IsParryable = true;

            // var parry = controller.GetComponent<ParrySystem>();
            // parry?.ActivateParry(); // 패링 타이밍 시작
        }

        // 일정 시간 후 패링 종료
        // if (elapsed >= startupTime + parryableTime)
        // {
        //     controller.IsParryable = false;
        // }

        if (elapsed >= attackDuration)
        {
            controller.TransitionTo(new IdleState(controller));
        }
    }

    public override void HandleInput() { }
    public override void Exit() { }
}
