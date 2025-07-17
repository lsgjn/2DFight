// PlayerController.cs
using UnityEngine;

/// <summary>
/// 플레이어의 FSM을 제어하는 핵심 클래스입니다.
/// 현재 상태를 추적하며 입력 처리와 상태 전환을 담당합니다.
/// </summary>
public class PlayerController : MonoBehaviour
{
    // 현재 상태
    private PlayerState currentState;

    // 컴포넌트 참조
    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    // 이동 관련 변수
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float runMultiplier = 1.5f;
    public float jumpForce = 7f;

    // 입력 값 (외부 입력 시스템이 처리해줄 수도 있음)
    [HideInInspector] public Vector2 inputDirection;
    [HideInInspector] public bool isShiftHeld;
    [HideInInspector] public bool jumpPressed;
    [HideInInspector] public bool guardPressed;
    [HideInInspector] public bool dodgePressed;

     [HideInInspector] public bool AttackPressed;

    /// <summary>
    /// 초기 설정: 기본 상태 지정 및 컴포넌트 캐싱
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 초기 상태는 IdleState
        TransitionTo(new IdleState(this));
    }

    /// <summary>
    /// 매 프레임마다 입력 처리 및 상태 로직 실행
    /// </summary>
    void Update()
    {
        ReadInput();                     // 입력 값을 읽고 저장
        currentState?.HandleInput();     // 현재 상태의 입력 처리
        currentState?.Update();          // 상태 유지 중 로직 실행
    }

    /// <summary>
    /// 입력 키를 읽어와 변수에 저장합니다.
    /// (원한다면 InputHandler로 따로 분리 가능)
    /// </summary>
    private void ReadInput()
    {
        inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        isShiftHeld = Input.GetKey(KeyCode.LeftShift);
        jumpPressed = Input.GetKeyDown(KeyCode.W);
        guardPressed = Input.GetKey(KeyCode.G);
        dodgePressed = Input.GetKeyDown(KeyCode.LeftAlt);
        AttackPressed = Input.GetKeyDown(KeyCode.F); // 공격 입력

        // 🛠️ 메커니즘 커스텀 지점:
        // 여기에 더 많은 입력을 추가하거나,
        // 버튼 연타, 홀드 시간 등을 체크해 확장할 수 있습니다.
    }

    /// <summary>
    /// 상태 전환을 수행합니다. 기존 상태는 Exit, 새 상태는 Enter를 호출합니다.
    /// </summary>
    public void TransitionTo(PlayerState newState)
    {
        currentState?.Exit();   // 기존 상태 정리
        currentState = newState;
        currentState.Enter();   // 새로운 상태 진입
    }

    /// 🔧 커스텀 메커니즘 확장 포인트 🔧
    /// - 상태 전환 조건을 FSM 외부에서 강제하고 싶을 때
    /// - 예: 공격 중 피격 시 상태 강제 전환 등
    /// public void ForceTransition(PlayerState overrideState) { ... }
}
