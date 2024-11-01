using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _baseDamage;
    [SerializeField]
    private float _randomnessFactor;
    private HealthController _healthController;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            OnHit(collision);
        }
        
        Destroy(gameObject);
    }

    private void OnHit(Collider2D collision)
    {
        _healthController = collision.GetComponent<HealthController>();
        float _damageAmount = _baseDamage * (1 - Random.Range(0f, _randomnessFactor));
        _healthController.TakeDamage(_damageAmount);
    }
}
