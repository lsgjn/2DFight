// using UnityEngine;
// using UnityEngine.UI;

// /// <summary>
// /// 가드 UI 바를 GuardSystem 수치에 따라 조정하는 컴포넌트
// /// (HealthBarUI.cs와 동일한 구조)
// /// </summary>
// public class GuardGaugeUI : MonoBehaviour
// {
//     [Header("가드 UI 설정")]
//     public GuardSystem target;   // 연결될 대상 (플레이어)
//     public Image fillBar;        // 채워지는 이미지

//     private void Update()
//     {
//         if (target == null || fillBar == null) return;

//         int currentGuard = Mathf.Clamp(target.GetCurrentGuard(), 0, target.maxGuard);
//         float ratio = (float)currentGuard / target.maxGuard;

//         fillBar.fillAmount = ratio;
//     }
// }
