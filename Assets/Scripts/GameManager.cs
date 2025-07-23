using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("플레이어 참조")]
    public PlayerController player1;
    public PlayerController player2;

    [Header("UI 요소")]
    public GameObject victoryPanel;  // 승리 UI 패널
    public TMP_Text victoryText;     // 승리 메시지를 표시할 텍스트

    private bool gameEnded = false;

    void Awake()
    {
        if (Instance == null) 
            Instance = this;
        else 
            Destroy(gameObject);
    }

    void Start()
    {
        // 게임 시작 시 PrefabBuilder에서 생성된 player1과 player2 참조 (이미 생성됨)
        // PrefabBuilder에서 player1과 player2가 GameManager에 할당됨
        // 이 부분은 따로 호출하지 않더라도 PrefabBuilder에서 GameManager.Instance.player1, player2에 할당됨.
    }

    void Update()
    {
        if (gameEnded) return;

        // 플레이어가 죽었는지 확인
        if (player1 != null && player1.GetComponent<DamageReceiver>().IsDead())
        {
            EndGame(player2); // 플레이어 2 승리
        }
        else if (player2 != null && player2.GetComponent<DamageReceiver>().IsDead())
        {
            EndGame(player1); // 플레이어 1 승리
        }
    }

    void EndGame(PlayerController winner)
    {
        gameEnded = true;

        // 승리 UI 표시
        if (victoryPanel != null && victoryText != null)
        {
            victoryPanel.SetActive(true);

            // 승리한 플레이어에 따라 메시지 설정
            if (winner.playerId == PlayerController.PlayerId.Player1)
            {
                victoryText.text = "1P Win!";
            }
            else if (winner.playerId == PlayerController.PlayerId.Player2)
            {
                victoryText.text = "2P Win!";
            }
        }

        // 게임 종료 후 캐릭터 선택 씬으로 전환
        Invoke("ReturnToCharacterSelect", 3f); // 게임 종료 후 캐릭터 선택 씬으로 전환
    }

    void ReturnToCharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelectScene");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬을 다시 로드
    }
}
