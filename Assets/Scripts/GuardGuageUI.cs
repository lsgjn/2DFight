using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어의 방어 게이지 UI 처리 (남은 가드 수에 따라 Bar 이미지 표시)
/// </summary>
public class GuardGaugeUI : MonoBehaviour
{
    [Header("대상 및 UI 바")]
    public GuardSystem target;        // 연결될 플레이어의 GuardSystem
    public Image[] guardBars;         // 개별 게이지 이미지들 (예: 3개)

    private int lastGuard = -1;

    void Update()
    {
        if (target == null || guardBars == null || guardBars.Length == 0)
            return;

        int current = Mathf.Clamp(target.GetCurrentGuard(), 0, guardBars.Length);

        if (current != lastGuard)
        {
            for (int i = 0; i < guardBars.Length; i++)
                guardBars[i].enabled = (i < current);

            lastGuard = current;
        }
    }
}
