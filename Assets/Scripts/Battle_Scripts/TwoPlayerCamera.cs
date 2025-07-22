using UnityEngine;

public class TwoPlayerCamera : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    [Header("Y Position Limit")]
    public float minY = 2f; // Y 최소값


    [Header("Zoom Settings")]
    public float minSize = 5f;       // 최소 줌 인 크기
    public float maxSize = 25f;      // 최대 줌 아웃 크기
    public float zoomSpeed = 5f;

    [Header("Follow Settings")]
    public float followSpeed = 5f;
    public float yOffset = 2f;       // 캐릭터 발이 보이도록 카메라 높이 보정

    [Header("Distance Zoom Factor")]
    public float zoomFactor = 0.5f;  // 거리 대비 줌 비율 (조정 가능)

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null) return;

        // ① 카메라 위치: 두 플레이어의 중간 + Y 오프셋
        Vector3 middle = (player1.position + player2.position) / 2f;
        Vector3 newPosition = new Vector3(middle.x, middle.y + yOffset, transform.position.z);

        
        // 📌 Y 위치 제한
        float targetY = Mathf.Max(middle.y + yOffset, minY);
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);

        // ② 카메라 줌: 두 캐릭터 거리 기반 계산 + factor 적용
        float distance = Vector2.Distance(player1.position, player2.position);
        float desiredSize = Mathf.Clamp(distance * zoomFactor, minSize, maxSize);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, desiredSize, zoomSpeed * Time.deltaTime);

    }
}
