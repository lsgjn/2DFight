using UnityEngine;
public class CombatResolver : MonoBehaviour
{
    public static CombatResolver Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ResolveHit(PlayerController attacker, PlayerController defender)
    {
        if (defender == null || attacker == null) return;

        // 1️⃣ 가드 판정
        var guard = defender.GetComponent<GuardSystem>();
        if (defender.IsGuarding && guard != null && guard.IsGuardAvailable())
        {
            guard.ConsumeGuard();

            // 가드 이펙트 출력
            GameObject effect = Object.Instantiate(Resources.Load<GameObject>("GuardEffect"),
                defender.transform.position, Quaternion.identity);
            Object.Destroy(effect, 1f);

            Debug.Log("🛡️ 가드 성공! 게이지 감소");
            return;
        }

        // 2️⃣ 패링 판정
        var parry = defender.GetComponent<ParrySystem>();
        if (parry != null && parry.IsParryActive() && attacker.IsParryable)
        {
            attacker.GetComponent<DamageReceiver>()?.ApplyStun();
            Debug.Log("⚡ 패링 성공! 공격자 스턴");
            return;
        }

        // 3️⃣ 일반 데미지 처리
        defender.GetComponent<DamageReceiver>()?.ApplyDamage(attacker.gameObject);
        Debug.Log("💥 데미지 적용 완료");
    }

}
