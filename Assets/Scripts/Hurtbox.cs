using UnityEngine;

/// <summary>
/// 피격 히트박스: 공격 히트박스에 닿으면 데미지 처리
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Hurtbox : MonoBehaviour
{
    void Awake()
    {
        var col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Hitbox>() != null)
        {
            Debug.Log("피격됨: " + gameObject.name);
            // TODO: 데미지, 넉백, 스턴 등 처리
        }
    }
}
