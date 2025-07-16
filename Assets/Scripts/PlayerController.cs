// PlayerController.cs (1P/2P 입력 통합 및 충돌 해결 버전)
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    private PlayerState currentState;
    private PlayerInputHandler input;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float runMultiplier = 1.5f;
    public float jumpForce = 7f;

    [HideInInspector] public Vector2 inputDirection;
    [HideInInspector] public bool isShiftHeld;
    [HideInInspector] public bool jumpPressed;
    [HideInInspector] public bool guardPressed;
    [HideInInspector] public bool dodgePressed;
    [HideInInspector] public bool attackHeld;

    private bool isChargingAttack = false;
    private float attackPressStartTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        input = GetComponent<PlayerInputHandler>();

        if (input == null)
        {
            Debug.LogError("PlayerInputHandler 컴포넌트가 없습니다.");
            enabled = false;
            return;
        }

        TransitionTo(new IdleState(this));
    }

    void Update()
    {
        ReadInput();
        currentState?.HandleInput();
        currentState?.Update();
    }

    private void ReadInput()
    {
        inputDirection = input.InputDirection;
        jumpPressed = input.JumpPressed;
        guardPressed = input.GuardPressed;
        dodgePressed = input.DodgePressed;
        attackHeld = input.AttackPressed;

        // 차징 공격 처리
        if (attackHeld && !isChargingAttack)
        {
            isChargingAttack = true;
            attackPressStartTime = Time.time;
        }
        else if (!attackHeld && isChargingAttack)
        {
            float heldTime = Time.time - attackPressStartTime;
            isChargingAttack = false;

            if (heldTime < 0.3f)
                TransitionTo(new AttackState(this));
            else
                TransitionTo(new ChargedAttackState(this, heldTime));
        }
    }

    public void TransitionTo(PlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public PlayerState CurrentState => currentState;
}
