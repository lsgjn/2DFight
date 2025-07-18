using UnityEngine;

/// <summary>
/// 모든 상태 클래스가 상속받는 기본 상태 클래스
/// </summary>
public abstract class PlayerState
{
    protected PlayerController controller;

    public PlayerState(PlayerController controller)
    {
        this.controller = controller;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void HandleInput();
    public abstract void Update();
}
