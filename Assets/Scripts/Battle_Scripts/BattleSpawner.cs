using UnityEngine;

public class BattlefieldSpawner : MonoBehaviour
{
    public Transform p1SpawnPoint;
    public Transform p2SpawnPoint;

    void Start()
{
    GameObject p1 = Instantiate(CharacterSelectData.Instance.p1Prefab, p1SpawnPoint.position, Quaternion.identity);
    GameObject p2 = Instantiate(CharacterSelectData.Instance.p2Prefab, p2SpawnPoint.position, Quaternion.identity);

    p1.name = "Player1";
    p2.name = "Player2";

    // PlayerInputHandler와 PlayerController 모두 playerId 할당
    p1.GetComponent<PlayerInputHandler>().playerId = PlayerInputHandler.PlayerId.Player1;
    p2.GetComponent<PlayerInputHandler>().playerId = PlayerInputHandler.PlayerId.Player2;

    p1.GetComponent<PlayerController>().playerId = PlayerController.PlayerId.Player1;
    p2.GetComponent<PlayerController>().playerId = PlayerController.PlayerId.Player2;

    var cam = Camera.main.GetComponent<TwoPlayerCamera>();
    cam.player1 = p1.transform;
    cam.player2 = p2.transform;

    GameManager.Instance.player1 = p1.GetComponent<PlayerController>();
    GameManager.Instance.player2 = p2.GetComponent<PlayerController>();
}
}
