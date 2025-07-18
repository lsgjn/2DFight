using UnityEngine;

/// <summary>
/// 방어 게이지 및 자동 회복 시스템
/// </summary>
public class GuardSystem : MonoBehaviour
{
    public int maxGuard = 3;
    public float rechargeInterval = 5f;

    private int currentGuard;
    private float rechargeTimer = 0f;

    void Awake()
    {
        currentGuard = maxGuard;
    }

    void Update()
    {
        if (currentGuard < maxGuard)
        {
            rechargeTimer += Time.deltaTime;
            if (rechargeTimer >= rechargeInterval)
            {
                rechargeTimer = 0f;
                currentGuard++;
            }
        }
    }

    public bool ConsumeGuard()
    {
        if (currentGuard > 0)
        {
            currentGuard--;
            rechargeTimer = 0f;
            return true;
        }
        return false;
    }

    public int GetCurrentGuard()
    {
        return currentGuard;
    }

    public bool IsGuardAvailable()
    {
        return currentGuard > 0;
    }
}
