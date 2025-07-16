using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

/// <summary>
/// 칼 타격 시 사용되는 히트박스 컴포넌트
/// DOTween을 이용해 궤적을 따라 움직이며 자동으로 꺼짐
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Hitbox : MonoBehaviour
{
    private BoxCollider2D col;
    private Vector3 originalLocalPos;

    [Header("히트박스 이동 설정")]
    public Vector2 localTargetOffset = new Vector2(1.0f, 0); // 칼 끝 방향 기준
    public float moveDuration = 0.1f;
    public float stayDuration = 0.15f;

    public Vector3[] pathPoints = new Vector3[] {
        new Vector3(0, 0, 0),
        new Vector3(-0.5f, 0.2f, 0),
        new Vector3(-1.0f, -0.2f, 0)
    };

    private List<Vector3> trailHistory = new List<Vector3>();
    private bool isMoving = false;

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
        col.isTrigger = true;
        originalLocalPos = transform.localPosition;
    }

    /// <summary>
    /// 히트박스를 궤적을 따라 움직이고 자동으로 꺼지는 실행 함수
    /// </summary>
    public void Activate()
    {
        StopAllCoroutines();
        transform.localPosition = originalLocalPos + pathPoints[0];
        col.enabled = true;

        Vector3[] adjustedPath = new Vector3[pathPoints.Length];
        for (int i = 0; i < pathPoints.Length; i++)
        {
            adjustedPath[i] = originalLocalPos + pathPoints[i];
        }

        trailHistory.Clear();
        isMoving = true;

        transform.DOLocalPath(adjustedPath, moveDuration, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                trailHistory.Add(transform.position);
            })
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(stayDuration, () =>
                {
                    transform.localPosition = originalLocalPos;
                    col.enabled = false;
                    isMoving = false;
                });
            });
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("타격 성공 → " + other.name);
            // TODO: 데미지 처리 연결
        }
    }

    void OnDrawGizmos()
    {
        // 실시간 이동 궤적 표시
        if (trailHistory.Count > 1)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < trailHistory.Count - 1; i++)
            {
                Gizmos.DrawLine(trailHistory[i], trailHistory[i + 1]);
            }
        }

        // 현재 히트박스의 콜라이더 표시
        var previewCol = GetComponent<BoxCollider2D>();
        if (previewCol != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, previewCol.size);
        }
    }
}
