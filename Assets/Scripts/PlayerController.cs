using UnityEngine;

/// <summary>
/// 캐릭터의 상태 FSM과 입력, 무기 정보를 제어하는 핵심 컨트롤러
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteAnimator))]
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    public enum PlayerId { Player1, Player2 }

    [Header("플레이어 설정")]
    public PlayerId playerId;
    public WeaponType weaponType;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteAnimator animator;
    [HideInInspector] public PlayerInputHandler input;
    [HideInInspector] public Hitbox swordHitbox;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    public bool IsParryable { get; set; }

    private PlayerState currentState;


public bool IsGuarding { get; private set; }
public void SetGuarding(bool value) => IsGuarding = value;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<SpriteAnimator>();
        input = GetComponent<PlayerInputHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        swordHitbox = transform.Find("SwordHitbox")?.GetComponent<Hitbox>();
        if (swordHitbox == null)
            Debug.LogError("SwordHitbox not found or Hitbox component missing!");
    }

    void Start()
    {
        TransitionTo(new IdleState(this));
    }

    void Update()
    {
        input.ReadInput();
        currentState?.HandleInput();
        currentState?.Update();
    }

    public void TransitionTo(PlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public bool IsGrounded()
    {
        // TODO: 실제 바닥 판정 구현 필요
        return true;
    }

    public void FaceDirection(float moveX)
    {
        if (moveX > 0.05f)
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        else if (moveX < -0.05f)
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
    }
}
