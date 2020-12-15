using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private EnemyMovement Movement;

    [SerializeField] private Transform _target;
    [SerializeField] private LayerMask _whatIsTargets;

    [SerializeField] private float _agrRadius;
    [SerializeField] private float _attackRate;
    private float _timeToAttack;

    private Vector3 _startPosition;
    private enum state
    {
        Walking,
        Chasing
    }
    state myState;

    void Start()
    {
        Movement = GetComponent<EnemyMovement>();

        _startPosition = transform.position;

        myState = state.Walking;
    }
    
    void Update()
    {
        if (myState == state.Walking)
        {
            CheckTargets();
            Movement.SetTarget(_startPosition);
            print("walking");
        }
        if (myState == state.Chasing)
        {
            CheckTargets();
            Movement.SetTarget(_target.position);
            print("Chasing");
        }
    }

    private void CheckTargets()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position , _agrRadius, _whatIsTargets);
        foreach(Collider target in targets)
        {
            if (target.tag == "Player")
            {
                print("player");
                _target = target.transform;
                myState = state.Chasing;
            }
        }
        if (targets.Length < 1)
        {
            print("zero targets");
            _target = null;
            myState = state.Walking;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position , _agrRadius);
    }
}
