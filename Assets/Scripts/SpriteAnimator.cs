using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SpriteAnimator : MonoBehaviour
{
    public enum AnimState
    {
        Idle,
        Run,
        Jump,
        Fall,
        Attack,
        Guard,
        // Dodge,
        Dead,
        ChargedAttack
    }

    private Animator animator;
    private AnimState currentState;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetState(AnimState newState)
    {
        if (newState == currentState) return;

        currentState = newState;
        animator.Play(newState.ToString());
    }
}
