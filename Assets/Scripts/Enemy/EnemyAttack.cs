using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;
    [SerializeField]
    private float _attackCooldown;
    bool attackReady = true;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && attackReady) {
            Debug.Log("Attacked");
            var HealthController = collision.gameObject.GetComponent<HealthController>();

            HealthController.TakeDamage(_damageAmount);
            StartCoroutine(AttackCooldownCoroutine());
        } else {Debug.Log("On Cooldown");}
    }


    private IEnumerator AttackCooldownCoroutine()
    {
        attackReady = false;
        yield return new WaitForSeconds(_attackCooldown);
        attackReady = true;
    }
}
