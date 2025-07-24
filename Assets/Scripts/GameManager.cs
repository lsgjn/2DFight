using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("플레이어 참조")]
    public PlayerController player1;
    public PlayerController player2;

    [Header("UI 요소")]
    public GameObject victoryPanel;     // 승리 UI 패널
    public Image victoryImage;          // 승리 이미지 (텍스트 대체)

    private bool gameEnded = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (gameEnded) return;

        if (player1 != null && player1.GetComponent<DamageReceiver>()?.IsDead() == true)
        {
            if (player2 != null)
                EndGame(player2); // 2P 승리
        }
        else if (player2 != null && player2.GetComponent<DamageReceiver>()?.IsDead() == true)
        {
            if (player1 != null)
                EndGame(player1); // 1P 승리
        }
    }

    void EndGame(PlayerController winner)
    {
        gameEnded = true;

        // 승리 UI 패널 표시
        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        if (victoryImage != null)
            victoryImage.gameObject.SetActive(true);

        // 카메라 포커싱
        var cam = Camera.main?.GetComponent<TwoPlayerCamera>();
        if (cam != null)
            cam.FocusOnWinner(winner.transform);
    }

    // 캐릭터 선택 씬으로 이동하는 버튼용 함수
    public void ReturnToCharacterSelect()
    {
        SceneManager.LoadScene("GameScene");  // 필요 시 이름 변경
    }

    // 재시작 버튼용 함수
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
