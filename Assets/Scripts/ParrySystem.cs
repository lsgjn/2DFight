// ✅ ParrySystem.cs
using UnityEngine;

public class ParrySystem : MonoBehaviour
{
    private bool isParryActive = false;
    private float parryTimer = 0f;
    private float cooldownTimer = 0f;

    public float parryWindow = 1f;   // 발동 시간
    public float cooldown = 5f;      // 쿨타임

    public bool IsParryActive() => isParryActive;

    public void ActivateParry()
    {
        if (cooldownTimer <= 0f)
        {
            isParryActive = true;
            parryTimer = parryWindow;
            cooldownTimer = cooldown;
            Debug.Log("🛡️ 패링 발동");
        }
    }

    private void Update()
    {
        if (isParryActive)
        {
            parryTimer -= Time.deltaTime;
            if (parryTimer <= 0f)
            {
                isParryActive = false;
                Debug.Log("패링 종료");
            }
        }

        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }
}
