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
    [SerializeField]
    private float _screenBorder;
    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _smoothMovement;
    private Vector2 _movementSmoothVelocity;
    private Vector3 _targetDirection;
    private float _changeDirectionCooldown;
    private Camera _camera;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _targetDirection = transform.up;
        _camera = Camera.main;
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
            HandleEnemyOffScreen();
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

    private void HandleEnemyOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < 0 && _targetDirection.x < 0) ||
        screenPosition.x > _camera.pixelWidth && _targetDirection.x > 0)
        {
            _targetDirection = new Vector2(-_targetDirection.x, _targetDirection.y);
        }
        if ((screenPosition.y < 0 && _targetDirection.y < 0) ||
        screenPosition.y > _camera.pixelHeight && _targetDirection.y > 0)
        {
            _targetDirection = new Vector2(_targetDirection.x, -_targetDirection.y);
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
