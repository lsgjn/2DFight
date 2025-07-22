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

    private Vector3 originalScale;


    public bool IsGuarding { get; private set; }
    public void SetGuarding(bool value) => IsGuarding = value;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<SpriteAnimator>();
        input = GetComponent<PlayerInputHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalScale = transform.localScale;  // ✅ 프리팹 크기 기억

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
        bool facingRight = moveX > 0.05f;

        // 스프라이트 반전
        if (facingRight)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        else if (moveX < -0.05f)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);

        // ✅ 히트박스 offset 반전
        if (swordHitbox != null)
        {
            var box = swordHitbox.GetComponent<BoxCollider2D>();
            if (box != null)
            {
                Vector2 originalOffset = box.offset;
                box.offset = new Vector2(
                    facingRight ? Mathf.Abs(originalOffset.x) : -Mathf.Abs(originalOffset.x),
                    originalOffset.y
                );
            }
        }
    }


}
