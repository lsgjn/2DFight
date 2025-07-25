using UnityEngine;
using UnityEngine.UI;
// 현재 main 씬에서 다른 씬으로 이동하는 스크립트
public class ToOtherScenes : MonoBehaviour
{
    // 이 스크립트는 다른 씬으로 이동하는 기능을 담당합니다.
    private Button gameStart;
    private Button gameExit;
    private Button gameSetting;
    //private Button storyGame;
    private void Awake()
    {
        SoundManager.Instance.PlayMainLoop();
        // 씬이 로드될 때 필요한 초기화 작업을 수행할 수 있습니다.
        // 예: 씬 전환 효과 설정 등
        gameStart = GameObject.Find("GameStart").GetComponent<Button>();
        gameExit = GameObject.Find("GameExit").GetComponent<Button>();
        //gameSetting = GameObject.Find("GameSetting").GetComponent<Button>();
        //storyGame = GameObject.Find("StoryGame").GetComponent<Button>();
    }
    public void LoadScene(string sceneName)
    {
        // UnityEngine.SceneManagement.SceneManager를 사용하여 씬을 로드합니다.
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void OnGameStartButtonClicked()
    {
        // 게임 시작 버튼 클릭 시 호출되는 메서드
        LoadScene("GameScene");
    }
    public void OnGameExitButtonClicked()
    {
        // 게임 종료 버튼 클릭 시 호출되는 메서드
        Application.Quit();
    }
    public void OnGameSettingButtonClicked()
    {
        // 게임 설정 버튼 클릭 시 호출되는 메서드
        LoadScene("SettingScene");
    }
    // public void OnStoryGameButtonClicked()
    // {
    //     // 스토리 게임 버튼 클릭 시 호출되는 메서드
    //     LoadScene("StoryScene");
    // }
    private void Start()
    {
        // 버튼 클릭 이벤트에 메서드를 연결합니다.
        gameStart.onClick.AddListener(OnGameStartButtonClicked);
        gameExit.onClick.AddListener(OnGameExitButtonClicked);
        //gameSetting.onClick.AddListener(OnGameSettingButtonClicked);
        //storyGame.onClick.AddListener(OnStoryGameButtonClicked);
    }

}
