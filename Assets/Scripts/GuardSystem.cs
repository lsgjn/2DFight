using UnityEngine;

public class GuardSystem : MonoBehaviour
{
    public int maxGuard = 90;
    public int currentGuard = 90;
    public float rechargeInterval = 5f;
    public int rechargeAmount = 30;

    private float rechargeTimer;

    void Start()
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
                currentGuard = Mathf.Min(currentGuard + rechargeAmount, maxGuard);
                rechargeTimer = 0f;
            }
        }
    }

    public float GetReductionRatio()
    {
        return currentGuard > 0 ? 0.3f : 1.0f;  // 30%로 경감
    }

    public int GetCurrentGuard() => currentGuard;
}