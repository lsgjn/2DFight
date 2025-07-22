// using UnityEngine;

// public class RandomSpriteBackground : MonoBehaviour
// {
//     [Header("사용할 배경 스프라이트들")]
//     public Sprite[] backgroundSprites;  // 여러 배경 스프라이트들

//     [Header("배경을 표시할 SpriteRenderer")]
//     public SpriteRenderer targetRenderer;  // 배경으로 쓸 SpriteRenderer

//     private void Start()
//     {
//         SetRandomBackground();
//     }

//     void SetRandomBackground()
//     {
//         if (backgroundSprites.Length == 0 || targetRenderer == null)
//         {
//             Debug.LogWarning("배경 스프라이트 또는 타겟 렌더러가 설정되지 않았습니다.");
//             return;
//         }

//         int index = Random.Range(0, backgroundSprites.Length);
//         targetRenderer.sprite = backgroundSprites[index];

//         AdjustScaleToScreen(); // 💡 추가
//     }

//     void AdjustScaleToScreen()
//     {
//         Sprite sprite = targetRenderer.sprite;
//         if (sprite == null) return;

//         // 카메라 기준 화면 사이즈 계산
//         Camera cam = Camera.main;
//         float screenHeight = cam.orthographicSize * 2f;
//         float screenWidth = screenHeight * cam.aspect;

//         // 스프라이트 원본 사이즈
//         float spriteWidth = sprite.bounds.size.x;
//         float spriteHeight = sprite.bounds.size.y;

//         // 화면에 맞는 스케일 계산
//         float scaleX = screenWidth / spriteWidth;
//         float scaleY = screenHeight / spriteHeight;

//         // 배경이 화면을 완전히 덮도록 더 큰 쪽으로 스케일 조정
//         float finalScale = Mathf.Max(scaleX, scaleY);

//         // 스프라이트 오브젝트에 적용
//         targetRenderer.transform.localScale = new Vector3(finalScale, finalScale, 1f);
//     }


// }
