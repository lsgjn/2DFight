using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private Button Back;
    private Button StartG;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        // 씬이 로드될 때 필요한 초기화 작업을 수행할 수 있습니다.
        // 예: 씬 전환 효과 설정 등
        Back = GameObject.Find("Back").GetComponent<Button>();
        StartG = GameObject.Find("StartG").GetComponent<Button>();
    }
    public void LoadScene(string sceneName)
    {
        // UnityEngine.SceneManagement.SceneManager를 사용하여 씬을 로드합니다.
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void OnGameStartGButtonClicked()
    {
        // 🔒 P1 또는 P2 프리팹이 비어 있으면 시작 금지
        if (CharacterSelectData.Instance == null || 
            CharacterSelectData.Instance.p1Prefab == null || 
            CharacterSelectData.Instance.p2Prefab == null)
        {
            Debug.LogWarning("캐릭터가 선택되지 않았습니다. 게임을 시작할 수 없습니다.");
            return;
        }

        LoadScene("Battle");
    }
    
    public void OnBackButtonClicked()
    {
        // 스토리 게임 버튼 클릭 시 호출되는 메서드
        LoadScene("Main");
    }
    private void Start()
    {
        // 버튼 클릭 이벤트에 메서드를 연결합니다.
        Back.onClick.AddListener(OnBackButtonClicked);
        StartG.onClick.AddListener(OnGameStartGButtonClicked);
    }

}
