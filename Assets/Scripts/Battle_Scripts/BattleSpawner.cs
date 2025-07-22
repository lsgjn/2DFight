
using UnityEngine;
public class BattlefieldSpawner : MonoBehaviour
{
    public Transform p1SpawnPoint;
    public Transform p2SpawnPoint;

    public GameObject p1IconPrefab; // 🔹 P1용 아이콘
    public GameObject p2IconPrefab; // 🔹 P2용 아이콘

    void Start()
    {
        var p1 = Instantiate(CharacterSelectData.Instance.p1Prefab, p1SpawnPoint.position, Quaternion.identity);
        var p2 = Instantiate(CharacterSelectData.Instance.p2Prefab, p2SpawnPoint.position, Quaternion.identity);

        p1.name = "Player1";
        p2.name = "Player2";

        var camera = Camera.main.GetComponent<TwoPlayerCamera>();
        camera.player1 = p1.transform;
        camera.player2 = p2.transform;

        // // 🔸 머리 위 흔들리는 아이콘 생성
        // Vector3 offset = new Vector3(0, 1.5f, 0);
        // Instantiate(p1IconPrefab, p1.transform.position + offset, Quaternion.identity, p1.transform);
        // Instantiate(p2IconPrefab, p2.transform.position + offset, Quaternion.identity, p2.transform);
    }
}
