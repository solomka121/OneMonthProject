using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private bool _takeDefaultVars = false;

    private Rigidbody _RB;
    private Collider _col;


    public LayerMask includeLayers;

    void Start()
    {
        if (!_takeDefaultVars)
        {
            _RB = GetComponent<Rigidbody>();
            _RB.AddForce(transform.forward * _speed , ForceMode.Impulse);

            _col = GetComponent<Collider>();
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
            enemyScipt.GetDamage(_damage , hitPoint);
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
