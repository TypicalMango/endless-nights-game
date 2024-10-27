using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;
    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private Vector2 _smoothMovement;
    private Vector2 _targetVelocity;
    private Vector2 _movementSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection() {
        if (_playerAwarenessController.AwareOfPlayer) {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
        else {
            _targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsTarget() {
        if (_targetDirection == Vector2.zero) {
            return;
        }
        transform.up = _targetDirection * _rotationSpeed;
        // Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        // Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        // _rigidbody.SetRotation(rotation);
    }


    private void SetVelocity()
    {
        _targetVelocity = CalculateVelocity();
        _smoothMovement = Vector2.SmoothDamp(
                    _smoothMovement,
                    _targetVelocity,
                    ref _movementSmoothVelocity,
                    0.1f
                );
        _rigidbody.velocity = _smoothMovement;
    }

    private Vector2 CalculateVelocity() {
        if (_targetDirection == Vector2.zero) {
            return Vector2.zero;
        }
        else {
            return transform.up * _speed;
        }

    }
}
