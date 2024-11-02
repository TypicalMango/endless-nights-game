using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }
    // [SerializeField]
    // private float _playerAwarenessDistance;
    private Transform _player;
    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>().transform;
    }


    // Update is called once per frame
    void Update()
    {
        IsTargetInsideFOV(_player);
    //     Vector2 enemyToPlayerVector = _player.position - transform.position;
    //     DirectionToPlayer = enemyToPlayerVector.normalized;

    //     if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance) {
    //         AwareOfPlayer = true;
    //     }
    //     else {
    //         AwareOfPlayer = false;
    //     }
    }

    [SerializeField]
    private float fovAngle;
    [SerializeField]
    private float fovRange;

    private Vector2 lookDirection = Vector2.up;

    private void IsTargetInsideFOV(Transform target)
    {
        Vector2 enemyToPlayerVector = target.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        float angleToPlayer = Vector2.Angle(lookDirection, DirectionToPlayer);

        if (angleToPlayer < fovAngle / 2 && enemyToPlayerVector.magnitude <= fovRange)
        {
            AwareOfPlayer = true;
        } else { AwareOfPlayer = false; }
    }

}
