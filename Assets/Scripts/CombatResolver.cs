// // ✅ Rewritten: CombatResolver.cs
// using UnityEngine;

// public class CombatResolver : MonoBehaviour
// {
//     public static CombatResolver Instance;

//     void Awake()
//     {
//         if (Instance == null) Instance = this;
//         else Destroy(gameObject);
//     }

//     public void ResolveHit(PlayerController attacker, PlayerController defender)
//     {
//         if (attacker == null || defender == null) return;

//         var guard = defender.GetComponent<GuardSystem>();
//         var damageReceiver = defender.GetComponent<DamageReceiver>();

//         // 1. 가드 경감 처리
//         // if (defender.IsGuarding && guard != null)
//         // {
//         //     //float reduction = guard.GetReductionRatio();
//         //     //damageReceiver?.ApplyDamage(attacker.gameObject, reduction);

//         //     // 🔄 가드 성공 시 캐릭터 하얗게 반짝이기
//         //     // var sprite = defender.GetComponent<SpriteRenderer>();
//         //     // if (sprite != null)
//         //     // {
//         //     //     defender.StartCoroutine(FlashWhite(sprite, 0.1f));
//         //     // }


//         //     //Debug.Log($"🛡️ 가드 경감 적용! 데미지 x{reduction}");
//         //     return;
//         // }
//         // else
//         // {
//         //     Debug.LogWarning("❌ GuardEffect prefab을 찾을 수 없습니다. Resources 폴더에 GuardEffect.prefab 파일을 넣어주세요.");
//         // }


//         // 2. 패링 판정
//         // var parry = defender.GetComponent<ParrySystem>();
//         // if (parry != null && parry.IsParryActive() && attacker.IsParryable)
//         // {
//         //     var attackerRB = attacker.GetComponent<Rigidbody2D>();
//         //     if (attackerRB != null)
//         //     {
//         //         Vector2 knockDir = (attacker.transform.position.x < defender.transform.position.x)
//         //             ? Vector2.left : Vector2.right;
//         //         attackerRB.linearVelocity = knockDir * 5f;
//         //     }

//         //     attacker.GetComponent<DamageReceiver>()?.ApplyStun();
//         //     Debug.Log($"✅ [패링 성공] {defender.name} → {attacker.name}");
//         //     return;
//         // }


//         // 3. 일반 데미지
//         // damageReceiver?.ApplyDamage(attacker.gameObject);
//         // Debug.Log("💥 일반 데미지 적용");

//     }
// }
// ✅ CombatResolver.cs
using UnityEngine;

public class CombatResolver : MonoBehaviour
{
    public static CombatResolver Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ResolveHit(PlayerController attacker, PlayerController defender)
    {
        if (attacker == null || defender == null) return;

        Debug.Log($"💥 Hit 발생: {attacker.name} -> {defender.name}");

        // 시각 효과만 담당
        defender.FlashRed();
    }
}