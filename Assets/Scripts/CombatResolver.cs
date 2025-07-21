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

        var guard = defender.GetComponent<GuardSystem>();

        if (defender.IsGuarding && guard != null && guard.IsGuardAvailable())
        {
            guard.ConsumeGuard();

            // 🌀 이펙트 출력 (예시 - GuardEffect 프리팹)
            GameObject effect = Object.Instantiate(Resources.Load<GameObject>("GuardEffect"),
                defender.transform.position, Quaternion.identity);
            Object.Destroy(effect, 1f); // 1초 후 파괴

            Debug.Log("🛡️ 가드 성공! 게이지 감소");
            return;
        }

        // 기존 패링
        var parry = defender.GetComponent<ParrySystem>();
        if (parry != null && parry.IsParryActive() && attacker.IsParryable)
        {
            attacker.GetComponent<DamageReceiver>()?.ApplyStun();
            return;
        }

        defender.GetComponent<DamageReceiver>()?.ApplyDamage(attacker.gameObject);
    }
}
