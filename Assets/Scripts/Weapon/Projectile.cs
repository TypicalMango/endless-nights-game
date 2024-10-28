using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            OnHit(collision);
        }
        
        Destroy(gameObject);
    }

    private static void OnHit(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
