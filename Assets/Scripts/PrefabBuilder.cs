using UnityEngine;

/// <summary>
/// 캐릭터 프리팹 생성 및 무기 타입 설정을 담당하는 빌더
/// </summary>
public class PrefabBuilder : MonoBehaviour
{
    public GameObject characterPrefab;
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    void Start()
    {
        string p1Str = PlayerPrefs.GetString("P1Char", "Katana");
        string p2Str = PlayerPrefs.GetString("P2Char", "Longsword");

        WeaponType p1 = (WeaponType)System.Enum.Parse(typeof(WeaponType), p1Str);
        WeaponType p2 = (WeaponType)System.Enum.Parse(typeof(WeaponType), p2Str);

        FindObjectOfType<PrefabBuilder>().BuildPlayers(p1, p2);

    }


    public void BuildPlayers(WeaponType p1Weapon, WeaponType p2Weapon)
    {
        GameObject p1 = Instantiate(characterPrefab, spawnPoint1.position, Quaternion.identity);
        InitPlayer(p1, PlayerController.PlayerId.Player1, p1Weapon);

        GameObject p2 = Instantiate(characterPrefab, spawnPoint2.position, Quaternion.identity);
        InitPlayer(p2, PlayerController.PlayerId.Player2, p2Weapon);

        GameManager.Instance.player1 = p1.GetComponent<PlayerController>();
        GameManager.Instance.player2 = p2.GetComponent<PlayerController>();
    }

    void InitPlayer(GameObject go, PlayerController.PlayerId id, WeaponType weapon)
    {
        var controller = go.GetComponent<PlayerController>();
        controller.playerId = id;
        controller.weaponType = weapon;

        float dir = (id == PlayerController.PlayerId.Player1) ? 1 : -1;
        go.transform.localScale = new Vector3(dir, 1, 1);
    }
}
