// PlayerState.cs
using UnityEngine;

/// <summary>
/// 모든 플레이어 상태의 공통 베이스 클래스입니다.
/// FSM의 핵심으로 각 상태는 이 클래스를 상속받아 구현됩니다.
/// </summary>
public abstract class PlayerState
{
    // 상태 전환 및 공통 변수에 접근하기 위한 컨트롤러 참조
    protected PlayerController controller;

    /// <summary>
    /// 상태 생성 시 컨트롤러를 주입받습니다.
    /// </summary>
    public PlayerState(PlayerController controller)
    {
        this.controller = controller;
    }

    /// <summary>
    /// 상태에 진입할 때 호출됩니다. (애니메이션 시작 등)
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// 상태에서 나올 때 호출됩니다. (애니메이션 종료 처리 등)
    /// </summary>
    public abstract void Exit();

    /// <summary>
    /// 입력을 처리합니다. 
    /// 키보드 입력이나 AI 입력 등은 여기에서 처리하고 상태 전환도 이곳에서 수행합니다.
    /// </summary>
    public abstract void HandleInput();

    /// <summary>
    /// 프레임마다 실행되는 로직입니다. 
    /// 예: 이동, 중력 처리 등 상태 유지 중의 행동
    /// </summary>
    public abstract void Update();

    /// 🔧 커스텀 메커니즘 확장 포인트 🔧
    /// 필요하다면 여기에 다음과 같은 메서드를 추가할 수 있습니다:
    /// - 상태 중 충돌 처리
    /// - 타이머 기반 이벤트 처리
    /// - 애니메이션 이벤트 처리
    /// 예: public virtual void OnCollisionEnter(Collision col) { ... }
}
