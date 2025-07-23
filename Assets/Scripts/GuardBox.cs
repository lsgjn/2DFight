using UnityEngine;

public class GuardBox : MonoBehaviour
{
    private GuardSystem guardSystem;

    void Awake()
    {
        guardSystem = GetComponentInParent<GuardSystem>();
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Hitbox"))
    {
        var attacker = other.GetComponentInParent<PlayerController>();
        var defender = GetComponentInParent<PlayerController>();

        if (attacker != null && defender != null)
        {
            // ✅ 이 한 줄만 실행
            CombatResolver.Instance.ResolveHit(attacker, defender);
        }
    }
}

}
