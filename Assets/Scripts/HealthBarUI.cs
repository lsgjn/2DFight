using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 체력 UI 바를 플레이어 HP에 따라 조정하는 컴포넌트
/// </summary>
public class HealthBarUI : MonoBehaviour
{
    [Header("체력 UI 설정")]
    public DamageReceiver target;  // 연결될 대상 (플레이어)
    public Image fillBar;          // 채워지는 이미지

    private void Update()
    {
        if (target == null || fillBar == null) return;

        float currentHP = Mathf.Clamp(target.GetCurrentHP(), 0, target.maxHP);
        float ratio = (float)currentHP / target.maxHP;

        fillBar.fillAmount = ratio;
    }
}
