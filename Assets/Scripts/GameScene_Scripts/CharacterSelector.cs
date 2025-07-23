using BBUnity.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characterPrefabs; // 프리팹 배열 추가

    public GameObject StartPanel;

    // === 선택 정보 ===
    private int p1Index = 0;
    private int p2Index = 0;

    private bool p1Confirmed = false;
    private bool p2Confirmed = false;
    

    // === 캐릭터 데이터 ===
    public Sprite[] characterSprites;
    public string[] characterNames;
    public string[] characterDescriptions;

    // === UI 참조 ===
    [Header("P1 UI")]
    public Image p1Image;
    public Text p1Name;
    public Text p1Description;

    [Header("P2 UI")]
    public Image p2Image;
    public Text p2Name;
    public Text p2Description;

    void Start()
    {
        UpdateUI(1);
        UpdateUI(2);
    }

    void Update()
    {
        // --- P1 입력 (A/D/W) ---
        if (!p1Confirmed)
        {
            if (Input.GetKeyDown(KeyCode.A)) { p1Index = Prev(p1Index); UpdateUI(1); }
            if (Input.GetKeyDown(KeyCode.D)) { p1Index = Next(p1Index); UpdateUI(1); }
            if (Input.GetKeyDown(KeyCode.Return)) { p1Confirmed = true; }
        }

        // --- P2 입력 (←/→/Enter) ---
        if (!p2Confirmed)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) { p2Index = Prev(p2Index); UpdateUI(2); }
            if (Input.GetKeyDown(KeyCode.RightArrow)) { p2Index = Next(p2Index); UpdateUI(2); }
            if (Input.GetKeyDown(KeyCode.KeypadEnter)) { p2Confirmed = true; }
        }

        // --- 둘 다 선택했을 때 게임 시작 ---
        if (p1Confirmed && p2Confirmed)
        {
            CharacterSelectData.Instance.p1Prefab = characterPrefabs[p1Index];
            CharacterSelectData.Instance.p2Prefab = characterPrefabs[p2Index];
            StartToPanel();
            //SceneManager.LoadScene("BattleScene");
        }
    }

    int Next(int index) => (index + 1) % characterSprites.Length;
    int Prev(int index) => (index - 1 + characterSprites.Length) % characterSprites.Length;

    void UpdateUI(int player)
    {
        int idx = (player == 1) ? p1Index : p2Index;
        if (player == 1)
        {
            p1Image.sprite = characterSprites[idx];
            p1Name.text = characterNames[idx];
            p1Description.text = characterDescriptions[idx];
        }
        else
        {
            p2Image.sprite = characterSprites[idx];
            p2Name.text = characterNames[idx];
            p2Description.text = characterDescriptions[idx];
        }
    }

    void StartToPanel()
    {
        StartPanel.SetActive(false);
    }
}
