using UnityEngine;
using System.Collections;

public class ChargingState : PlayerState
{
    private float chargeTime = 1.5f;
    private float elapsed = 0f;
    private bool chargedFeedbackPlayed = false;

    public ChargingState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        elapsed = 0f;
        controller.rb.linearVelocity = Vector2.zero;
        controller.animator.SetState(SpriteAnimator.AnimState.Idle);
    }

    public override void HandleInput()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= chargeTime && !chargedFeedbackPlayed)
        {
            controller.StartCoroutine(FlashYellow(controller.spriteRenderer));
            chargedFeedbackPlayed = true;
        }

        // 키를 뗐을 경우
        if (!controller.input.AttackHeld)
        {
            if (elapsed >= chargeTime)
                controller.TransitionTo(new ChargedAttackState(controller));
            else
                controller.TransitionTo(new AttackState(controller));
        }
    }

    public override void Update()
    {
        controller.FaceDirection(controller.input.InputDirection.x);
    }

    private IEnumerator FlashYellow(SpriteRenderer sr)
    {
        Color original = sr.color;
        Color yellow = Color.yellow;

        for (int i = 0; i < 2; i++)
        {
            sr.color = yellow;
            yield return new WaitForSeconds(0.1f);
            sr.color = original;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void Exit() { }
}
