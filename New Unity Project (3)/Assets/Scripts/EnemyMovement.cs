using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody _RB;

    private NavMeshAgent _NM; 

    private Vector3 _Target;

    void Start()
    {
        _RB = GetComponent<Rigidbody>();
        _NM = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        /*_Target.y = 0;
        Vector3 dir = _Target.normalized;
        _RB.velocity = dir * _speed;*/
    }

    public void SetTarget(Vector3 target)
    {
        _Target = target;
        _NM.SetDestination(_Target);
    }

    public void StopMoving()
    {
        _NM.isStopped = true;
    }
}
