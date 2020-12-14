using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private LayerMask _whatIsTargets;
    [SerializeField] private float _agrRadius;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _attackRate;
    private float _timeToAttack;
    private enum state
    {
        Walking,
        Chasing
    }
    state myState;

    void Start()
    {
        myState = state.Walking;
    }
    
    void Update()
    {
        if (myState == state.Walking)
        {
            CheckTargets();
        }
    }

    private void CheckTargets()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position , _agrRadius, _whatIsTargets);
        foreach(Collider target in targets)
        {
            if (target.tag == "Player")
            {
                _target = target.transform;
            }
        }
        if (targets == null)
        {
            _target = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position , _agrRadius);
    }
}
