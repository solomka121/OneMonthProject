using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody _RB;
    private Vector3 _point;
    [SerializeField] private float _speed;

    void Start()
    {
        _RB = GetComponent<Rigidbody>();
        // _point = transform.position;
        _point = new Vector3(10 , 0 , 0 );
    }

    void FixedUpdate()
    {
        _point.y = 0;
        Vector3 dir = _point.normalized;
        _RB.velocity = dir * _speed;
    }
}
