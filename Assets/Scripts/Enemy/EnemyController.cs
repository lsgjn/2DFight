using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyState currentState;

    [Header("AI 설정")]
    public Transform player;
    public float moveSpeed = 3f;
    public float chaseRange = 5f;
    public float attackRange = 1.2f;

    [Header("컴포넌트")]
    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        TransitionTo(new EnemyIdleState(this)); // 처음에는 대기 상태
    }

    void Update()
    {
        currentState?.Update();
    }

    public void TransitionTo(EnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
