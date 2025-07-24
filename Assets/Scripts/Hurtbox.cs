// ✅ Hurtbox.cs
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public void ReceiveHit(GameObject attacker)
    {
        Debug.Log($"[Hurtbox] 피격: {gameObject.name} <- {attacker.name}");

        var damageReceiver = GetComponentInParent<DamageReceiver>();
        var guard = GetComponentInParent<GuardSystem>();
        var parry = GetComponentInParent<ParrySystem>();
        var attackerController = attacker.GetComponent<PlayerController>();

        float reduction = 1f;

        // ✅ 1. 패링 상태라면 → 데미지 0
        if (parry != null && parry.IsParryActive()) // && attackerController != null && attackerController.IsParryable
        {
            reduction = 0f;
            Debug.Log("🛡️ 패링 성공 - 데미지 무효");
            parry.OnParrySuccess();
            var playerController = GetComponentInParent<PlayerController>();
            playerController?.FlashRed(true);
        }
        // ✅ 2. 가드 상태라면 → 데미지 1/4
        else if (guard != null && guard.IsGuarding())
        {
            reduction = 0.25f;
            Debug.Log("🛡️ 가드 상태 - 데미지 1/4 적용");
        }

        damageReceiver?.ApplyDamage(attacker, reduction);
    }
}
