using UnityEngine;

/// <summary>
/// 공격 vs 방어/패링 충돌을 중앙에서 판단하는 전투 해석기
/// </summary>
public class CombatResolver : MonoBehaviour
{
    public static CombatResolver Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// 히트 충돌 시 패링 또는 데미지 판단
    /// </summary>
    public void ResolveHit(PlayerController attacker, PlayerController defender)
    {
        if (defender == null || attacker == null) return;

        var defenderParry = defender.GetComponent<ParrySystem>();
        var attackerDamage = attacker.GetComponent<DamageReceiver>();
        var defenderDamage = defender.GetComponent<DamageReceiver>();

        // 패링 성공 조건: 방어자가 패링 상태 && 공격자가 패링 당할 수 있음
        if (defenderParry != null && defenderParry.IsParryActive() && attacker.IsParryable)
        {
            Debug.Log("💥 패링 성공! → " + attacker.name + " 스턴됨");
            attackerDamage?.ApplyStun();
            return;
        }

        // 그 외에는 정상적인 공격 처리
        defenderDamage?.ApplyDamage(attacker.gameObject);
    }
}
