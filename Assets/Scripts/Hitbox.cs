using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Animator))]
public class Hitbox : MonoBehaviour
{
    private BoxCollider2D col;
    //private Animator anim;

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        //anim = GetComponent<Animator>();
        col.enabled = false;
        col.isTrigger = true;
    }

    public void Activate()
    {
        col.enabled = true;
        //anim.Play("AttackHitbox");
        Invoke(nameof(Disable), 0.3f); // 공격 판정 지속 시간
    }

    private void Disable()
    {
        col.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("🔥 OnTriggerEnter2D 호출됨: " + other.name);
        // 공격 ↔ 공격 충돌
        if (other.TryGetComponent(out Hitbox enemyHitbox))
        {
            var attacker = GetComponentInParent<PlayerController>();
            var defender = enemyHitbox.GetComponentInParent<PlayerController>();

            if (attacker != null && defender != null)
            {
                var parry = defender.GetComponent<ParrySystem>();
                if (parry != null && parry.IsParryActive() && attacker.IsParryable)
                {
                    attacker.GetComponent<DamageReceiver>()?.ApplyStun();
                    attacker.rb.linearVelocity = (attacker.transform.position.x < defender.transform.position.x)
                        ? Vector2.left * 5f : Vector2.right * 5f;

                    Debug.Log("⚡ [패링 성공] 공격 간 충돌로 발동!");
                    ShowParryMessage(defender);
                    return;
                }

                Debug.Log("⚔️ [공격 상쇄] 패링 실패 또는 조건 미충족");
                return;
            }
        }

        // 공격 ↔ 헐트박스 충돌 처리
        // Hurtbox.cs로 직접 전달
        if (other.TryGetComponent(out Hurtbox hurtbox))
        {
            var defender = hurtbox.GetComponentInParent<PlayerController>();
            var attacker = GetComponentInParent<PlayerController>();

            if (defender != null && attacker != null)
            {
                var parry = defender.GetComponent<ParrySystem>();
                if (parry != null && parry.IsParryActive() && attacker.IsParryable)
                {
                    attacker.GetComponent<DamageReceiver>()?.ApplyStun();
                    attacker.rb.linearVelocity = (attacker.transform.position.x < defender.transform.position.x)
                        ? Vector2.left * 5f : Vector2.right * 5f;

                    Debug.Log("⚡ [패링 성공] 가드 중 발동!");
                    return;
                }

                // ✅ 핵심 수정: CombatResolver 대신 hurtbox.ReceiveHit 호출
                hurtbox.ReceiveHit(attacker.gameObject);
                Debug.Log($"⭐ Hit 발생: {attacker.name} → {defender.name}");
                SoundManager.Instance.PlayHurt();
            }
        }


    }

    private void ShowParryMessage(PlayerController player)
    {
        Debug.Log($"✅ {player.playerId} 패링 성공!");
    }
}
