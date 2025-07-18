
using UnityEngine;
public class BattlefieldSpawner : MonoBehaviour
{
    public Transform p1SpawnPoint;
    public Transform p2SpawnPoint;

    void Start()
    {
        var p1 = Instantiate(CharacterSelectData.Instance.p1Prefab, p1SpawnPoint.position, Quaternion.identity);
        var p2 = Instantiate(CharacterSelectData.Instance.p2Prefab, p2SpawnPoint.position, Quaternion.identity);

        // 네이밍, 태그 설정 등
        p1.name = "Player1";
        p2.name = "Player2";
    }
}
