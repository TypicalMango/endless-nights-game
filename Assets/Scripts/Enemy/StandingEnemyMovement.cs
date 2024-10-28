using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StandingEnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;
    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _smoothMovement;
    private Vector2 _movementSmoothVelocity;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }


    private void FixedUpdate()
    {
        if (_playerAwarenessController.AwareOfPlayer) {
            // Rotate towards target
            transform.up = _playerAwarenessController.DirectionToPlayer * _rotationSpeed;

            SetVelocity(transform.up * _speed);
        }
        else {
            SetVelocity(Vector2.zero);
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
