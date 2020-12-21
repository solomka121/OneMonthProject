using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private EnemyMovement Movement;

    [SerializeField] private Transform _eyesPoint;

    [SerializeField] private float _agrRadius;
    [SerializeField] private Transform _target;
    [SerializeField] private LayerMask _whatIsTargets;
    [SerializeField] private LayerMask _hitTargets;
    [SerializeField] private LayerMask _eyesFilter;


    [SerializeField] private int _meleeDamage;
    [SerializeField] private float _meleeRange;
    [SerializeField] private float _meleeAttackRate;
    [SerializeField] private Transform _meleeAttackPoint;
    private float _timeToMeleeAttack;

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

        _timeToMeleeAttack = _meleeAttackRate + Time.time;

        myState = state.Walking;
    }

    public void PauseCombatAI(float delay)
    {
        StartCoroutine(startAiDelay(delay));
        this.enabled = false;
    }
    
    void Update()
    {
        if (myState == state.Walking)
        {
            CheckTargets();
            Movement.SetTarget(_startPosition);
        }
        if (myState == state.Chasing)
        {
            CheckTargets();
            Movement.SetTarget(_target.position);
            if (_timeToMeleeAttack <= Time.time)
            {
                if (Vector3.Distance(_meleeAttackPoint.position, _target.position) <= _meleeRange)
                {
                    MeleeAttack();
                    _timeToMeleeAttack = _meleeAttackRate + Time.time;
                }
            }
        }
    }

    private void MeleeAttack()
    {
        Collider[] hitted = Physics.OverlapSphere(_meleeAttackPoint.position , _meleeRange , _hitTargets);
        foreach(Collider hit in hitted)
        {
            PlayerHealth PH;
            if (PH = hit.GetComponent<PlayerHealth>())
            {
                //Vector3 hitPoint = hit.ClosestPoint(transform.position);
                PH.GetDamage(_meleeDamage , transform.position);
            }
        }
    }

    private void CheckTargets()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position , _agrRadius, _whatIsTargets);
        foreach(Collider target in targets)
        {
            if (target.tag == "Player")
            {
                RaycastHit rayHit;
                Vector3 dirToTarget = target.transform.position - transform.position;
                Physics.Raycast(_eyesPoint.position, dirToTarget.normalized , out rayHit, Mathf.Infinity ,  _eyesFilter);
                Debug.DrawRay(_eyesPoint.position, dirToTarget.normalized * rayHit.distance);
                if (rayHit.collider.gameObject.tag == target.gameObject.tag)
                {
                    _target = target.transform;
                    myState = state.Chasing;
                }

            }
        }
        if (targets.Length < 1)
        {
            _target = null;
            myState = state.Walking;
        }
    }

    private IEnumerator startAiDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position , _agrRadius);

        Gizmos.color = new Color(1, 0, 0, 1f);
        Gizmos.DrawWireSphere(_meleeAttackPoint.position , _meleeRange);
    }
}
