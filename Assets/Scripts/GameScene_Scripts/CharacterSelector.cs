using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characterPrefabs;      // 선택 가능한 캐릭터 프리팹들
    public Transform displayPosition;          // 캐릭터를 보여줄 위치

    private int currentIndex = 0;              // 현재 선택 중인 캐릭터 인덱스
    private GameObject currentCharacter;       // 현재 화면에 보이는 캐릭터

    void Start()
    {
        ShowCharacter(currentIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentIndex = (currentIndex + 1) % characterPrefabs.Length;
            ShowCharacter(currentIndex);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentIndex = (currentIndex - 1 + characterPrefabs.Length) % characterPrefabs.Length;
            ShowCharacter(currentIndex);
        }

        if (Input.GetKeyDown(KeyCode.Return)) // Enter 키로 선택
        {
            PlayerPrefs.SetInt("SelectedCharacterIndex", currentIndex);
            SceneManager.LoadScene("GameScene"); // 실제 게임 씬 이름으로 변경
        }
    }

    void ShowCharacter(int index)
    {
        if (currentCharacter != null)
            Destroy(currentCharacter);

        currentCharacter = Instantiate(characterPrefabs[index], displayPosition.position, Quaternion.identity);
        currentCharacter.transform.SetParent(displayPosition); // 부모 지정 (선택사항)
    }
}
