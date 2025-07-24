using Unity.VisualScripting;
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
    public bool GuardHeld { get; private set; }

    public bool DashPressed{ get; private set; }

    public void ReadInput()
    {
        if (playerId == PlayerId.Player1)
        {
            // 1P: W (점프), A/D (이동), F (공격), G (가드)
            InputDirection = new Vector2(
                Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0,
                Input.GetKey(KeyCode.W) ? 1 : 0
            );
            JumpPressed = Input.GetKeyDown(KeyCode.W);
            GuardPressed = Input.GetKey(KeyCode.G);
            GuardHeld = Input.GetKey(KeyCode.G);
            AttackPressed = Input.GetKeyDown(KeyCode.F);
            DashPressed = Input.GetKeyDown(KeyCode.H);
        }
        else if (playerId == PlayerId.Player2)
        {
            // 2P: ↑ (점프), ←→ (이동), Keypad1 (공격), Keypad2 (가드)
            InputDirection = new Vector2(
                Input.GetKey(KeyCode.RightArrow) ? 1 : Input.GetKey(KeyCode.LeftArrow) ? -1 : 0,
                Input.GetKey(KeyCode.UpArrow) ? 1 : 0
            );
            JumpPressed = Input.GetKeyDown(KeyCode.UpArrow);
            GuardPressed = Input.GetKey(KeyCode.Keypad2);
            GuardHeld = Input.GetKey(KeyCode.Keypad2);
            AttackPressed = Input.GetKeyDown(KeyCode.Keypad1);
            DashPressed = Input.GetKeyDown(KeyCode.Keypad3);
        }
    }
}
