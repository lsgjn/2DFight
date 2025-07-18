using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 체력 UI 바를 플레이어 HP에 따라 조정하는 컴포넌트
/// </summary>
public class HealthBarUI : MonoBehaviour
{
    public DamageReceiver target;
    public Image fillBar;

    private int lastHP = -1;

    void Update()
    {
        if (target == null || fillBar == null) return;

        int current = Mathf.Max(0, target.IsDead() ? 0 : target.GetCurrentHP());
        if (current != lastHP)
        {
            fillBar.fillAmount = (float)current / target.maxHP;
            lastHP = current;
        }
    }
}
