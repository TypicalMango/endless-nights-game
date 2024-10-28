using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoamingEnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;
    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _smoothMovement;
    private Vector2 _movementSmoothVelocity;
    private Vector3 _targetDirection;
    private float _changeDirectionCooldown;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _targetDirection = transform.up;
    }


    private void FixedUpdate()
    {
        if (_playerAwarenessController.AwareOfPlayer) {
            // Rotate towards target
            transform.up = _playerAwarenessController.DirectionToPlayer * _rotationSpeed;
            SetVelocity(transform.up * _speed);
        }
        else {
            HandleRandomDirectionChange();
            transform.up = _targetDirection * _rotationSpeed;
            SetVelocity(transform.up * _speed);
        }
    }


    private void HandleRandomDirectionChange()
    {
        _changeDirectionCooldown -= Time.deltaTime;

        if (_changeDirectionCooldown <= 0) {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            _targetDirection = rotation * _targetDirection;

            _changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }


    private void SetVelocity(Vector2 _targetVelocity)
    {
        _rigidbody.velocity = Vector2.SmoothDamp(
                    _smoothMovement,
                    _targetVelocity,
                    ref _movementSmoothVelocity,
                    0.1f
                );
    }
}
