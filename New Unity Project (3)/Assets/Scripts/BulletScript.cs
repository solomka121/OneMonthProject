using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private bool _takeDefaultVars = false;
    
    private Rigidbody _RB;

    void Start()
    {
        if (!_takeDefaultVars)
        {
            _RB = GetComponent<Rigidbody>();
            _RB.velocity = transform.forward * _speed;
        }
        else
        {

        }
    }

    public void Init(int damage, float speed)
    {
        _damage = damage;
        _speed = speed;
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
        else if (other.gameObject.tag == "RoomZone")
        {
            //nothing
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
