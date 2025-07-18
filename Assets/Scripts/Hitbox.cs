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
        anim.Play("HitboxSlash");
        Invoke(nameof(Disable), 0.15f); // 애니메이션 길이만큼만 판정 유지
    }

    private void Disable()
    {
        col.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Hurtbox hurtbox))
        {
            var defender = hurtbox.GetComponentInParent<PlayerController>();
            var attacker = GetComponentInParent<PlayerController>();
            CombatResolver.Instance.ResolveHit(attacker, defender);
        }
    }
}
