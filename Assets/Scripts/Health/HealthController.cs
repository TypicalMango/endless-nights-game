using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private float _maximumHealth;
    public float RemainingHealthPercentage
    {
        get {
            return _currentHealth / _maximumHealth;
        }
    }
    public bool IsInvincible { get; set; }


    public UnityEvent OnHealthZero;
    public UnityEvent OnDamaged;


    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth == 0) {
            return;
        }

        if (IsInvincible) {
            return;
        }

        _currentHealth -= damageAmount;

        if (_currentHealth < 0) {
            _currentHealth = 0;
        }

        if (_currentHealth == 0) {
            OnHealthZero.Invoke();
        }
        else {
            OnDamaged.Invoke();
        }
    }


    public void AddHealth(float addAmount)
    {
        if (_currentHealth == _maximumHealth) {
            return;
        }
        _currentHealth += addAmount;
        if (_currentHealth > _maximumHealth) {
            _currentHealth = _maximumHealth;
        }
    }
}
