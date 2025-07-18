using UnityEngine;

/// <summary>
/// 타이밍 기반 패링 시스템 - 성공 시 상대 스턴
/// </summary>
public class ParrySystem : MonoBehaviour
{
    public float parryWindow = 0.2f; // 패링 타이밍 허용 범위
    public float parryCooldown = 1.0f; // 실패 후 재사용 대기

    private float parryTimer = 0f;
    private bool parryActive = false;
    private float cooldownTimer = 0f;

    public void TryParry()
    {
        if (cooldownTimer > 0f) return;

        parryActive = true;
        parryTimer = parryWindow;
        cooldownTimer = parryCooldown;
    }

    void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        if (parryActive)
        {
            parryTimer -= Time.deltaTime;
            if (parryTimer <= 0f)
                parryActive = false;
        }
    }

    public bool IsParryActive()
    {
        return parryActive;
    }

    public bool CanParry()
    {
        return cooldownTimer <= 0f;
    }
}
