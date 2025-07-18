using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectUI : MonoBehaviour
{
    public void OnSelectP1Katana() => PlayerPrefs.SetString("P1Char", "Katana");
    public void OnSelectP1Longsword() => PlayerPrefs.SetString("P1Char", "Longsword");

    public void OnSelectP2Katana() => PlayerPrefs.SetString("P2Char", "Katana");
    public void OnSelectP2Longsword() => PlayerPrefs.SetString("P2Char", "Longsword");

    public void OnConfirm() => SceneManager.LoadScene("BattleScene");
}