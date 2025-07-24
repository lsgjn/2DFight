using UnityEngine;

public class TwoPlayerCamera : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    [Header("Zoom Settings")]
    public float minSize = 15f;
    public float maxSize = 50f;
    public float zoomSpeed = 5f;

    [Header("Follow Settings")]
    public float followSpeed = 5f;

    [Tooltip("화면 높이를 낮추려면 음수로 설정")]
    public float yOffset = -1.5f;

    [Header("Distance Zoom Factor")]
    public float zoomFactor = 0.5f;

    [Header("Winner Follow Offset")]
public float winnerYOffset = -0.5f; // 승자 팔로우 시 더 많이 내려감


    private Camera cam;

    // 승자 따라가기용 변수
    private Transform winnerTarget = null;
    private bool isWinnerFocus = false;
    private float winnerZoomSize = 8f; // 원하는 줌 크기

    void Start()
    {
        cam = GetComponent<Camera>();
    }

   void LateUpdate()
{
    if (isWinnerFocus && winnerTarget != null)
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, winnerZoomSize, zoomSpeed * Time.deltaTime);
        Vector3 targetPos = new Vector3(
            winnerTarget.position.x,
            winnerTarget.position.y + winnerYOffset, // 승자 팔로우 시만 winnerYOffset 사용
            transform.position.z
        );
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        return;
    }

        if (player1 == null || player2 == null) return;

        // 두 플레이어 중심 위치
        Vector3 middle = (player1.position + player2.position) / 2f;

        // 거리 계산 (줌용)
        float distance = Vector2.Distance(player1.position, player2.position);
        float desiredSize = Mathf.Clamp(distance * zoomFactor, minSize, maxSize);

        // 카메라 줌 부드럽게 적용
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, desiredSize, zoomSpeed * Time.deltaTime);

        // 카메라가 모든 플레이어를 포함하도록 위치 조정
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        float minX = Mathf.Min(player1.position.x, player2.position.x);
        float maxX = Mathf.Max(player1.position.x, player2.position.x);
        float minY = Mathf.Min(player1.position.y, player2.position.y);
        float maxY = Mathf.Max(player1.position.y, player2.position.y);

        float targetX = (minX + maxX) / 2f;
        float targetY = (minY + maxY) / 2f + yOffset;

        Vector3 newPosition = new Vector3(targetX, targetY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);
    }

    // 승자 따라가기 시작
    public void FocusOnWinner(Transform winner)
    {
        winnerTarget = winner;
        isWinnerFocus = true;
    }
}