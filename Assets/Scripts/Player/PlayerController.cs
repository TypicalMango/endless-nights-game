// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Numerics;
// using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
// using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _walkSpeed = 3f;
    private Vector2 _movementInput;
    private Vector2 _smoothMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    Rigidbody2D _rigidbody;
    private float _playerHealth;
    private Vector3 mousePosition;
    private Animator _animator;
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _animator = GetComponentInChildren<Animator>();
        _camera = Camera.main;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        SetPlayerVelocity();
        SetAnimation();
        RotatePlayerToMouse();
    }


    private void SetAnimation()
    {
        bool isMoving = _movementInput != Vector2.zero;
        _animator.SetBool("IsMoving", isMoving);
    }


    private void RotatePlayerToMouse()
    {
        if (Camera.main != null)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        transform.up = direction;
    }
    else
    {
        Debug.LogWarning("Main Camera is not assigned in the scene.");
    }
    }


    private void SetPlayerVelocity()
    {
        _smoothMovementInput = Vector2.SmoothDamp(
                    _smoothMovementInput,
                    _movementInput,
                    ref _movementInputSmoothVelocity,
                    0.1f
                );
        _rigidbody.velocity = _smoothMovementInput * _walkSpeed;
        PreventPlayerGoingOffScreen();
    }

    private void PreventPlayerGoingOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < 0 && _rigidbody.velocity.x < 0) ||
        screenPosition.x > _camera.pixelWidth && _rigidbody.velocity.x > 0)
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }
        if ((screenPosition.y < 0 && _rigidbody.velocity.y < 0) ||
        screenPosition.y > _camera.pixelHeight && _rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        }
    }


    private void OnMove(InputValue inputValue) {
        _movementInput = inputValue.Get<Vector2>();
    }
}
