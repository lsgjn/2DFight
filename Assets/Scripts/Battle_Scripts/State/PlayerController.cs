using UnityEngine;
using System.Collections;

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

    [Header("캐릭터 능력치")]
    public float moveSpeed = 6f; // 프리팹별 이동 속도
    public float jumpForce = 7f; // 프리팹별 점프력
    public float dodgeSpeed = 8f; // 프리팹별 회피 속도
    public float attackSpeed = 1f; // 프리팹별 공격 속도 또는 딜레이

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteAnimator animator;
    [HideInInspector] public PlayerInputHandler input;
    [HideInInspector] public Hitbox swordHitbox;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    [HideInInspector] public GameObject guardBox;

    public bool IsParryable { get; set; }

    private PlayerState currentState;

    private Vector3 originalScale;

    public bool IsGuarding { get; private set; }
    public void SetGuarding(bool value) => IsGuarding = value;

    public GameObject deathEffectPrefab;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<SpriteAnimator>();
        input = GetComponent<PlayerInputHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalScale = transform.localScale;

        swordHitbox = transform.Find("SwordHitbox")?.GetComponent<Hitbox>();
        if (swordHitbox == null)
            Debug.LogError("SwordHitbox not found or Hitbox component missing!");

        // 🔽 2. guardBox 자동 연결 (이름이 "GuardBox"인 자식 GameObject 찾아 연결)
        guardBox = transform.Find("GuardBox")?.gameObject;
        if (guardBox == null)
            Debug.LogWarning("GuardBox not found under Player prefab!");
    }

    void Start()
    {
        TransitionTo(new IdleState(this));

        // 체력 UI 연결
        var health = GetComponentInChildren<HealthBarUI>();
        if (health != null)
            health.target = GetComponent<DamageReceiver>();

        // 가드 UI 연결
        var guard = GetComponentInChildren<GuardGaugeUI>();
        if (guard != null)
            guard.target = GetComponent<GuardSystem>();
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
    
    public void FlashRed()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        if (spriteRenderer == null) yield break;

        Color originalColor = spriteRenderer.color;
        Color hitColor = Color.red;

        float flashDuration = 0.1f;
        int flashCount = 2;

        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = hitColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }

}
