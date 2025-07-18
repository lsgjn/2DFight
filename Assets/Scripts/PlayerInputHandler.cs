using UnityEngine;

/// <summary>
/// 플레이어 입력 처리 (1P/2P 구분 포함)
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    public enum PlayerId { Player1, Player2 }
    public PlayerId playerId = PlayerId.Player1;

    public Vector2 InputDirection { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool GuardPressed { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool DodgePressed { get; private set; }

    public void ReadInput()
    {
        // 1P: WASD / 2P: ArrowKeys
        if (playerId == PlayerId.Player1)
        {
            InputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            JumpPressed = Input.GetKeyDown(KeyCode.W);
            GuardPressed = Input.GetKey(KeyCode.LeftControl);
            AttackPressed = Input.GetKeyDown(KeyCode.F);
            DodgePressed = Input.GetKeyDown(KeyCode.LeftAlt);
        }
        else if (playerId == PlayerId.Player2)
        {
            InputDirection = new Vector2(
                Input.GetKey(KeyCode.RightArrow) ? 1 : Input.GetKey(KeyCode.LeftArrow) ? -1 : 0,
                Input.GetKey(KeyCode.UpArrow) ? 1 : Input.GetKey(KeyCode.DownArrow) ? -1 : 0
            );
            JumpPressed = Input.GetKeyDown(KeyCode.UpArrow);
            GuardPressed = Input.GetKey(KeyCode.RightControl);
            AttackPressed = Input.GetKeyDown(KeyCode.Keypad1);
            DodgePressed = Input.GetKeyDown(KeyCode.Keypad2);
        }
    }
}
