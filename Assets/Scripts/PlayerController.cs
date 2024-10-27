using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float maxHealth = 100f;
    public float walkSpeed = 3f;
    float horzInput;
    float vertInput;
    Rigidbody2D rb;
    float playerHealth;
    Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = maxHealth;
        // mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        horzInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
        if (horzInput != 0 && vertInput != 0)
            {
            rb.velocity = new Vector2(horzInput * walkSpeed / Mathf.Sqrt(2), vertInput * walkSpeed / Mathf.Sqrt(2));
        }
        else if (horzInput != 0 || vertInput != 0)
            {
            rb.velocity = new Vector2(horzInput * walkSpeed, vertInput * walkSpeed);
        }
        // else
        // {
        //     rb.velocity = Vector2.zero;
        // }
    }

    void LateUpdate() {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        transform.up = direction;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Coillison");
        playerHealth -= 10;
        Debug.Log(playerHealth);
    }
}
