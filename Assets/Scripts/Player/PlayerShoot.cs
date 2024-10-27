using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private float _projectileSpeed;
    [SerializeField]
    private Transform _weaponOffset;
    [SerializeField]
    private float _timeBetweenShots;
    private bool _shootContinuously;
    private bool _shootSingle;
    private float _lastShootTime;

    void Update()
    {
        if (_shootContinuously || _shootSingle)
        {
            float timeSinceLastShot = Time.time - _lastShootTime;
            if (timeSinceLastShot >= _timeBetweenShots) {
                shootProjectile();
                _lastShootTime = Time.time;
                _shootSingle = false;
            }
        }
    }

    private void shootProjectile()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _weaponOffset.position, transform.rotation);
        Rigidbody2D rigidbody = projectile.GetComponent<Rigidbody2D>();

        rigidbody.velocity = _projectileSpeed * transform.up;
    }

    private void OnFire(InputValue inputValue)
    {
        _shootContinuously = inputValue.isPressed;

        if (inputValue.isPressed) {
            _shootSingle = true;
        }
    }
}
