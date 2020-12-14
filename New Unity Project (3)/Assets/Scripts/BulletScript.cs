using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody _RB;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    void Start()
    {
        _RB = GetComponent<Rigidbody>();
        _RB.velocity = transform.forward * _speed;
    }
    void FixedUpdate()
    {
        //transform.Translate(transform.forward * 10 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyScipt;
        if (enemyScipt = other.gameObject.GetComponent<EnemyHealth>())
        {
            enemyScipt.GetDamage(_damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
