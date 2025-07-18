using UnityEngine;

/// <summary>
/// 피격 시 데미지 및 넉백, 스턴 효과 처리
/// </summary>
public class DamageReceiver : MonoBehaviour
{
    public int maxHP = 3;
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

    public void ApplyDamage(GameObject attacker)
    {
        if (isStunned) return;

        currentHP--;
        Debug.Log(gameObject.name + " 피격! 남은 체력: " + currentHP);

        // 넉백 방향 계산
        Vector2 knockDir = (transform.position.x > attacker.transform.position.x) ? Vector2.right : Vector2.left;
        controller.rb.linearVelocity = knockDir * knockbackForce;

        // 스턴 처리
        isStunned = true;
        stunTimer = stunDuration;
    }

    public void ApplyStun()
    {
        Debug.Log(gameObject.name + " 패링에 의해 스턴!");
        controller.rb.linearVelocity = Vector2.zero;
        isStunned = true;
        stunTimer = stunDuration;
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

    public bool IsDead()
    {
        return currentHP <= 0;
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }
}
