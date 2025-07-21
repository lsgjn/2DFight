using UnityEngine;

/// <summary>
/// FSM 상태 전이 테스트 자동화 도구 (디버그용)
/// </summary>
public class FSMTestRunner : MonoBehaviour
{
    public PlayerController player;

    void Start()
    {
        if (player == null) player = FindObjectOfType<PlayerController>();
        StartCoroutine(RunFSMTest());
    }

    System.Collections.IEnumerator RunFSMTest()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("▶ FSM 테스트 시작");

        player.TransitionTo(new IdleState(player));
        yield return WaitState("Idle", 1f);

        player.TransitionTo(new RunState(player));
        yield return WaitState("Run", 1f);

        player.TransitionTo(new JumpState(player));
        yield return WaitState("Jump", 0.6f);

        player.TransitionTo(new FallState(player));
        yield return WaitState("Fall", 1f);

        player.TransitionTo(new GuardState(player));
        yield return WaitState("Guard", 0.6f);

        player.TransitionTo(new AttackState(player));
        yield return WaitState("Attack", 0.5f);

        // player.TransitionTo(new DodgeState(player));
        // yield return WaitState("Dodge", 0.4f);

        player.TransitionTo(new IdleState(player));
        Debug.Log("✅ FSM 테스트 완료");
    }

    System.Collections.IEnumerator WaitState(string name, float time)
    {
        Debug.Log("⏱ 상태: " + name);
        yield return new WaitForSeconds(time);
    }
}
