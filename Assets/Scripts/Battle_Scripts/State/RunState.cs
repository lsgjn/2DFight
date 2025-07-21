// // RunState.cs
// using UnityEngine;

// /// <summary>
// /// 플레이어가 좌우로 이동 중인 상태입니다.
// /// 이동 방향, 속도, 그리고 Shift에 따른 달리기 속도를 처리합니다.
// /// </summary>
// public class RunState : PlayerState
// {
//     public RunState(PlayerController controller) : base(controller) { }

//     public override void Enter()
//     {
//         controller.animator.Play("Run");

//         // 🛠️ 커스텀 포인트:
//         // - 이펙트나 발소리 재생
//         // - runStart 사운드 등도 여기에서 재생 가능
//         // 예: AudioManager.Instance.Play("RunStart");
//     }

//     public override void Exit()
//     {
//         // 상태 종료 시 이동 관련 효과 정리 가능
//         // 예: AudioManager.Instance.Stop("RunLoop");
//     }

//     public override void HandleInput()
//     {
//         Vector2 input = controller.inputDirection;

//         // 입력이 없어지면 Idle로 전환
//         if (input.x == 0f)
//         {
//             controller.TransitionTo(new IdleState(controller));
//             return;
//         }

//         // 점프
//         if (controller.jumpPressed)
//         {
//             controller.TransitionTo(new JumpState(controller));
//             return;
//         }

//         // 가드
//         if (controller.guardPressed)
//         {
//             controller.TransitionTo(new GuardState(controller));
//             return;
//         }

//         // 회피
//         if (controller.dodgePressed)
//         {
//             controller.TransitionTo(new DodgeState(controller));
//             return;
//         }

//         // 🛠️ 커스텀 포인트:
//         // - 빠르게 달리는 도중에는 공격 불가 등 제약을 넣을 수도 있음
//     }

//     public override void Update()
//     {
//         Vector2 input = controller.inputDirection;

//         // 이동 방향 결정
//         float direction = Mathf.Sign(input.x);
//         float baseSpeed = controller.moveSpeed;

//         // Shift 키 입력 여부에 따라 속도 증가
//         float moveSpeed = controller.isShiftHeld ? baseSpeed * controller.runMultiplier : baseSpeed;

//         // 이동 적용
//         controller.rb.linearVelocity = new Vector2(direction * moveSpeed, controller.rb.linearVelocity.y);

//         // 캐릭터 방향 반영
//         if (direction != 0)
//             controller.spriteRenderer.flipX = direction < 0;

//         // 🛠️ 커스텀 포인트:
//         // - Dust Trail 생성
//         // - 발걸음 소리 타이밍 조절
//         // - 달리기 중 스태미너 소비 구현 가능
//     }
// }
