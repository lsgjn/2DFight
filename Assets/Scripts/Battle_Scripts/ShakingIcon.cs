// using UnityEngine;

// public class ShakingIcon : MonoBehaviour
// {
//     public float duration = 0.5f;
//     public float magnitude = 1f;
//     public float speed = 10f;

//     private Vector3 startPos;
//     private float elapsed = 0f;

//     void Start()
//     {
//         startPos = transform.localPosition;
//     }

//     void Update()
//     {
//         if (elapsed < duration)
//         {
//             elapsed += Time.deltaTime;
//             float y = Mathf.Sin(Time.time * speed) * magnitude;
//             transform.localPosition = startPos + new Vector3(0, y, 0);
//         }
//         else
//         {
//             Destroy(gameObject); // 끝나면 자동 제거
//         }
//     }
// }
