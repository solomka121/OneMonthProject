using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private bool _takeDefaultVars = false;

    private Rigidbody _RB;

    public void Init(int damage, float speed)
    {
        if (!_takeDefaultVars)
        {
            _damage = damage;
            _speed = speed;
        }
        else
        {

        }
    }
    void Start()
    {
        _RB = GetComponent<Rigidbody>();
        _RB.velocity = transform.forward * _speed;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth PH;
        if (PH = other.GetComponent<PlayerHealth>())
        {
            PH.GetDamage(_damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
