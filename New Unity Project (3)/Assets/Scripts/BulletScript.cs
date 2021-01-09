using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _stunChance;
    [SerializeField] private float _stunDuration;

    private Rigidbody _RB;
    private Collider _col;


    public LayerMask includeLayers;

    void Start()
    {
        _RB = GetComponent<Rigidbody>();
        _RB.AddForce(transform.forward * _speed , ForceMode.Impulse);

        _col = GetComponent<Collider>();
    }

    public void Init(int damage, float speed)
    {
        _damage = damage;
        _speed = speed;
    }
    public void Init(int damage, float speed , float stunChance , float stunDuration)
    {
        _damage = damage;
        _speed = speed;
        _stunChance = stunChance;
        _stunDuration = stunDuration;
    }
    void FixedUpdate()
    {
        //transform.Translate(transform.forward * 10 * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision other)
    {
        

        if (((1 << other.gameObject.layer) & includeLayers) != 0)
        {
            //It matched one
        }

        EnemyHealth enemyScipt;
        if (enemyScipt = other.gameObject.GetComponent<EnemyHealth>())
        {
            Vector3 hitPoint = _col.ClosestPoint(other.transform.position);
            if (_stunChance <= 0)
            {
                enemyScipt.GetDamage(_damage, hitPoint);
            }
            else
            {
                enemyScipt.GetDamage(_damage, hitPoint , _stunChance , _stunDuration);
            }
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
