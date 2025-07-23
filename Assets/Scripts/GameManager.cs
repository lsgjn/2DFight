using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 승패 관리 및 라운드 종료를 담당하는 게임 매니저
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController player1;
    public PlayerController player2;

    public GameObject victoryPanel;
    public TMPro.TextMeshProUGUI victoryText;

    private bool gameEnded = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (gameEnded) return;

        if (player1.GetComponent<DamageReceiver>().IsDead())
        {
            EndGame(player2);
        }
        else if (player2.GetComponent<DamageReceiver>().IsDead())
        {
            EndGame(player1);
        }
    }

    void EndGame(PlayerController winner)
    {
        gameEnded = true;

        if (victoryPanel != null && victoryText != null)
        {
            victoryPanel.SetActive(true);
            victoryText.text = winner.playerId + " Wins!";
        }

        // 입력/이동/애니메이션 중지 등 후속 처리 가능
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
