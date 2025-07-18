using UnityEngine;

/// <summary>
/// 피격 판정을 위한 헐트박스 - 피격 시 데미지 처리 호출
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Hurtbox : MonoBehaviour
{
    public void ReceiveHit(GameObject attacker)
    {
        Debug.Log("피격됨 → " + gameObject.name + " by " + attacker.name);

        var damageReceiver = GetComponentInParent<DamageReceiver>();
        if (damageReceiver != null)
        {
            damageReceiver.ApplyDamage(attacker);
        }
    }
}
