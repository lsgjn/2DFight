using UnityEngine;

/// <summary>
/// 공격 중 자동 발동형 패링 시스템
/// </summary>
public class ParrySystem : MonoBehaviour
{
    public float parryWindow = 0.12f;      // 약 1.5~2프레임 (13fps 기준)
    public float cooldown = 0.5f;          // 재사용 대기시간

    private float timer = 0f;
    private bool active = false;
    private float cooldownTimer = 0f;

    public void ActivateParry()
    {
        if (cooldownTimer > 0f) return;

        active = true;
        timer = parryWindow;
        cooldownTimer = cooldown;
    }

    public bool IsParryActive() => active;

    void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        if (active)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                active = false;
        }
    }
}
