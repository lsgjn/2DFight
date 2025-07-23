using UnityEngine;

/// <summary>
/// 플레이어 사망 시 상태 처리 (애니메이션 포함)
/// </summary>
public class DeathState : PlayerState
{
    private bool animationPlayed = false;

    public DeathState(PlayerController controller) : base(controller) {}

    public override void Enter()
    {
        controller.rb.linearVelocity = Vector2.zero;
        controller.rb.isKinematic = true;
        controller.rb.simulated = false;

        foreach (var col in controller.GetComponentsInChildren<Collider2D>())
        {
            col.enabled = false;
        }

        controller.input.enabled = false;

        // 🔥 죽음 이펙트 실행
        if (controller.deathEffectPrefab != null)
        {
            Debug.Log("sds");
            GameObject effect = GameObject.Instantiate(
                controller.deathEffectPrefab,
                controller.transform.position,
                Quaternion.identity
            );
            GameObject.Destroy(effect, 2f); // 2초 후 제거
        }

        controller.animator.SetState(SpriteAnimator.AnimState.Dead);
    }


    public override void Exit() {}

    public override void HandleInput() {}

    public override void Update()
    {
        // 사망 상태에서는 아무 입력도 받지 않음
    }
}
