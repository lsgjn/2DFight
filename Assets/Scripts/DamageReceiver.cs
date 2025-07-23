using UnityEngine;
public class DamageReceiver : MonoBehaviour
{
    public int maxHP = 12;
    private int currentHP;

    public float knockbackForce = 5f;
    public float stunDuration = 0.5f;

    private PlayerController controller;
    private bool isStunned = false;
    private float stunTimer = 0f;

    void Awake()
    {
        currentHP = maxHP;
        controller = GetComponent<PlayerController>();
    }

    // public void ApplyDamage(GameObject attacker)
    // {
    //     ApplyDamage(attacker, 1.0f);
    // }

    public void ApplyDamage(GameObject attacker, float reductionRatio)
    {
        if (isStunned) return;
        int baseDamage = 4;

        int damage = Mathf.CeilToInt(baseDamage * reductionRatio);
        currentHP -= damage;
        Debug.Log($"❤️ 데미지 {damage} 적용됨, 남은 체력: {currentHP}");
        

        // int baseDamage = 1;
        // int damage = Mathf.Max(1, Mathf.CeilToInt(baseDamage * reduction));
        // currentHP -= damage;

        Debug.Log($"{gameObject.name} 피격! 남은 체력: {currentHP} (-{damage})");

        // 🔴 피격 시 깜빡이게
        controller.FlashRed();

        if (currentHP <= 0)
        {
            controller.TransitionTo(new DeathState(controller));
            return;
        }

        Vector2 knockDir = (transform.position.x > attacker.transform.position.x) ? Vector2.right : Vector2.left;
        controller.rb.linearVelocity = knockDir * knockbackForce;

        isStunned = true;
        stunTimer = stunDuration;

        if (IsDead())
        {
            // controller.TransitionTo(new DeathState(controller)); // 주석 처리됨
        }
    }


    public void ApplyStun()
    {
        controller.rb.linearVelocity = Vector2.zero;
        isStunned = true;
        stunTimer = stunDuration;

        Debug.Log($"{gameObject.name} 스턴!");
    }

    void Update()
    {
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                isStunned = false;
            }
        }
    }

    public bool IsDead() => currentHP <= 0;
    public int GetCurrentHP() => currentHP;
}