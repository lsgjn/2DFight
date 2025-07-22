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
            if (guardSystem.ConsumeGuard())
            {
                // 가드 성공 이펙트/사운드
                Debug.Log("Guard successful!");
            }
            else
            {
                // 가드 실패 → 피해 입기 등 처리 가능
            }
        }
    }
}
