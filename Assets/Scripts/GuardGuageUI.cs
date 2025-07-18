using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어의 방어 게이지 UI 처리
/// </summary>
public class GuardGaugeUI : MonoBehaviour
{
    public GuardSystem target;
    public Image[] guardBars; // 3칸이라면 Image 3개

    private int lastGuard = -1;

    void Update()
    {
        if (target == null || guardBars.Length == 0) return;

        int guard = target.GetCurrentGuard();
        if (guard != lastGuard)
        {
            for (int i = 0; i < guardBars.Length; i++)
            {
                guardBars[i].enabled = (i < guard);
            }
            lastGuard = guard;
        }
    }
}
