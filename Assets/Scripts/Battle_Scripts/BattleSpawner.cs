using UnityEngine;

public class BattlefieldSpawner : MonoBehaviour
{
    public Transform p1SpawnPoint;
    public Transform p2SpawnPoint;

    void Start()
    {
        GameObject p1 = Instantiate(CharacterSelectData.Instance.p1Prefab, p1SpawnPoint.position, Quaternion.identity);
        GameObject p2 = Instantiate(CharacterSelectData.Instance.p2Prefab, p2SpawnPoint.position, Quaternion.identity);

        // 이름 지정
        p1.name = "Player1";
        p2.name = "Player2";

        // Player ID 설정
        p1.GetComponent<PlayerInputHandler>().playerId = PlayerInputHandler.PlayerId.Player1;
        p2.GetComponent<PlayerInputHandler>().playerId = PlayerInputHandler.PlayerId.Player2;

        // 카메라 추적용 대상 설정
        var cam = Camera.main.GetComponent<TwoPlayerCamera>();
        cam.player1 = p1.transform;
        cam.player2 = p2.transform;
    }
}
