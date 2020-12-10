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
        _RB.velocity = transform.forward * 100 * _speed * Time.deltaTime;
    }
    void FixedUpdate()
    {
        //transform.Translate(transform.forward * 10 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Enemy en;
        if (en = other.gameObject.GetComponent<Enemy>())
        {
            en.GetDamage(_damage);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player" || other.gameObject.tag == "Gun")
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }

}
