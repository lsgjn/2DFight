using UnityEngine;

/// <summary>
/// 플레이어의 공격 상태를 정의합니다.
/// 공격 애니메이션 재생 및 타이머를 통해 상태 전환을 처리합니다.
/// </summary>
public class AttackState : PlayerState
{
    private float attackDuration = 0.5f;  // 공격 애니메이션 지속 시간
    private float elapsedTime = 0f;

    private bool animationTriggered = false;

    public AttackState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        elapsedTime = 0f;
        animationTriggered = false;

        // 공격 애니메이션 실행
        controller.animator.Play("Attack");

        // 🛠️ 확장 포인트: 무기 종류에 따라 다른 애니메이션을 재생할 수 있음
    }

    public override void Exit()
    {
        // 필요 시 종료 애니메이션이나 리셋 처리
    }

    public override void HandleInput()
    {
        // 공격 중에는 입력을 무시하거나 제한할 수 있음
        // 예: 이동, 점프 입력 무시
    }

    public override void Update()
    {
        elapsedTime += Time.deltaTime;

        // 애니메이션 이벤트 또는 타이머로 히트박스 생성
        if (!animationTriggered && elapsedTime >= 0.2f)
        {
            TriggerAttack();
            animationTriggered = true;
        }

        // 공격이 끝나면 Idle 상태로 복귀
        if (elapsedTime >= attackDuration)
        {
            controller.TransitionTo(new IdleState(controller));
        }
    }

    private void TriggerAttack()
    {
        // 히트박스 생성 로직 또는 공격 판정
        Debug.Log("🔺 공격 히트박스 활성화");

        // 🛠️ 확장 포인트:
        // - 방향에 따른 히트박스 생성
        // - 무기/콤보/타격 이펙트 반영
    }
}
