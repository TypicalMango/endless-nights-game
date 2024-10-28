using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Player Position: " + transform.position);
        SetPlayerVelocity();
        RotatePlayerToMouse();

        // OLD MOVEMENT SCRIPT

        // horzInput = Input.GetAxisRaw("Horizontal");
        // vertInput = Input.GetAxisRaw("Vertical");
        // if (horzInput != 0 && vertInput != 0)
        //     {
        //     _rigidbody.velocity = new Vector2(horzInput * _walkSpeed / Mathf.Sqrt(2), vertInput * _walkSpeed / Mathf.Sqrt(2));
        // }
        // else if (horzInput != 0 || vertInput != 0)
        //     {
        //     _rigidbody.velocity = new Vector2(horzInput * _walkSpeed, vertInput * _walkSpeed);
        // }
        // else
        // {
        //     _rigidbody.velocity = Vector2.zero;
        // }
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
    }

    private void OnMove(InputValue inputValue) {
        _movementInput = inputValue.Get<Vector2>();
    }
}
