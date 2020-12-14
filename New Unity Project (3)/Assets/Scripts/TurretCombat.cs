using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCombat : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _attackSpeed = 1;
    private float _timeToShoot;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _barrelPoint;

    [SerializeField] private Transform _target;
    [SerializeField] private float _agrRadius;
    public LayerMask isTargets;
    void Start()
    {
        _timeToShoot = _attackSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_target == null)
        {
            checkTargets();
        }
        else
        {
            Vector3 smoothedTargetPos = Vector3.Lerp(_barrelPoint.position , _target.position , 0.05f);
            _head.LookAt(smoothedTargetPos);
            checkTargets();
            if (_timeToShoot <= 0 )
            {
                var bull = Instantiate(_bullet , _barrelPoint.position , _barrelPoint.rotation);
                bull.GetComponent<EnemyBullet>().Init(_damage , _bulletSpeed);
                _timeToShoot = _attackSpeed;
            }
            else
            {
                _timeToShoot -= Time.deltaTime;
            }
        }

        Debug.DrawRay(_barrelPoint.position , _barrelPoint.forward * 10f);
    }
    private void checkTargets()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, _agrRadius, isTargets);
        foreach (Collider target in targets)
        {
            _target = target.transform;
        }
        if (targets.Length == 0)
        {
            _target = null;
        }
    }
    
    private void Shoot()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1 , 0 , 0 , 0.5f);
        Gizmos.DrawWireSphere(transform.position , _agrRadius);
    }
}
