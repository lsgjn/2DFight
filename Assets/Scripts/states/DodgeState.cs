using UnityEngine;

public class DodgeState : PlayerState
{
    private float dodgeTime = 0.3f;
    private float dodgeSpeed = 8f;
    private float elapsed = 0f;
    private int direction;

    public DodgeState(PlayerController controller) : base(controller) {}

    public override void Enter()
    {
        controller.animator.SetState(SpriteAnimator.AnimState.Dodge);
        elapsed = 0f;

        // 입력 방향이 없을 경우 현재 바라보는 방향으로 회피
        direction = (int)Mathf.Sign(controller.input.InputDirection.x);
        if (direction == 0)
            direction = controller.transform.localScale.x > 0 ? 1 : -1;
    }

    public override void Exit()
    {
        // 회피 종료 시 효과음 중지, 무적 해제 등 처리 가능
    }

    public override void HandleInput()
    {
        // 회피 중에는 입력 무시 (또는 공격 전환 가능하도록 수정 가능)
    }

    public override void Update()
    {
        elapsed += Time.deltaTime;

        var input = controller.input;
        // ✅ 수정됨
        controller.rb.linearVelocity = new Vector2(direction * dodgeSpeed, controller.rb.linearVelocity.y);

        if (elapsed >= dodgeTime)
        {
            controller.TransitionTo(new IdleState(controller));
        }
        controller.FaceDirection(controller.input.InputDirection.x);
    }
}
