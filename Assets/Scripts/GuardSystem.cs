// ✅ GuardSystem.cs
using UnityEngine;

public class GuardSystem : MonoBehaviour
{
    private PlayerInputHandler input;
    private ParrySystem parry;

    private bool isGuarding = false;
    public bool IsGuarding() => isGuarding;

    private void Awake()
    {
        input = GetComponent<PlayerInputHandler>();
        parry = GetComponent<ParrySystem>();
    }

    private void Update()
    {
        HandleGuardInput();
    }

    private void HandleGuardInput()
    {
        if (input == null) return;

        // 가드 키를 누른 순간 → 패링 발동
        if (input.GuardPressed)
            parry?.ActivateParry();

        // 가드 키를 누르고 있는 동안 → 가드 상태 유지
        isGuarding = input.GuardHeld;
    }
}