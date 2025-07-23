using UnityEngine;
[RequireComponent(typeof(BoxCollider2D), typeof(Animator))]
public class Hitbox : MonoBehaviour
{
    private BoxCollider2D col;
    private Animator anim;

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        col.enabled = false;
        col.isTrigger = true;
    }

    public void Activate()
    {
        col.enabled = true;
        anim.Play("AttackHitbox");
        Invoke(nameof(Disable), 0.3f); // 공격 판정 지속 시간 (ex. 0.3초)
    }

    private void Disable()
    {
        col.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hitbox 충돌 발생: " + other.name);
        if (other.TryGetComponent(out Hurtbox hurtbox))
        {
            var defender = hurtbox.GetComponentInParent<PlayerController>();
            var attacker = GetComponentInParent<PlayerController>();
            if (defender != null && attacker != null)
            {
                CombatResolver.Instance.ResolveHit(attacker, defender);
            }
        }
    }
}
