using UnityEngine;

public class TwoPlayerCamera : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    public float minSize = 5f;       // 최소 줌 인 크기
    public float maxSize = 15f;      // 최대 줌 아웃 크기
    public float zoomSpeed = 5f;
    public float followSpeed = 5f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null) return;

        // ① 카메라 위치: 두 플레이어의 중간
        Vector3 middle = (player1.position + player2.position) / 2f;
        Vector3 newPosition = new Vector3(middle.x, middle.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);

        // ② 카메라 줌: 거리 기반
        float distance = Vector2.Distance(player1.position, player2.position);
        float desiredSize = Mathf.Clamp(distance, minSize, maxSize);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, desiredSize, zoomSpeed * Time.deltaTime);
    }
}
