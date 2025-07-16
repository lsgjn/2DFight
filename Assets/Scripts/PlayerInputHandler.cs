using UnityEngine;

/// <summary>
/// 플레이어 1P, 2P의 키 입력을 처리하는 독립 클래스입니다.
/// PlayerController는 여기서 읽은 입력값만 받아 처리합니다.
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    public enum PlayerType { Player1, Player2 }
    public PlayerType playerType = PlayerType.Player1;

    public Vector2 InputDirection { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool GuardPressed { get; private set; }
    public bool DodgePressed { get; private set; }

    void Update()
    {
        switch (playerType)
        {
            case PlayerType.Player1:
                InputDirection = new Vector2(
                    (Input.GetKey(KeyCode.D) ? 1 : 0) + (Input.GetKey(KeyCode.A) ? -1 : 0),
                    (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0)
                );
                JumpPressed = Input.GetKeyDown(KeyCode.W);
                AttackPressed = Input.GetKey(KeyCode.F);
                GuardPressed = Input.GetKey(KeyCode.G);
                DodgePressed = Input.GetKeyDown(KeyCode.LeftAlt);
                break;

            case PlayerType.Player2:
                InputDirection = new Vector2(
                    (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) + (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0),
                    (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) + (Input.GetKey(KeyCode.DownArrow) ? -1 : 0)
                );
                JumpPressed = Input.GetKeyDown(KeyCode.UpArrow);
                AttackPressed = Input.GetKey(KeyCode.Keypad1);
                GuardPressed = Input.GetKey(KeyCode.Keypad2);
                DodgePressed = Input.GetKeyDown(KeyCode.RightControl);
                break;
        }
    }
}
