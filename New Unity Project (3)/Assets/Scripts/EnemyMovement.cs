using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public ParticleSystem footSteps;
    private ParticleSystem.EmissionModule footStepsEmission;

    public EnemyHealth enemyHealth;

    private Rigidbody _RB;
    private NavMeshAgent _NM;
    private Animator _anim;

    private MeshRenderer _skinMaterial;
    private Vector3 _Target;

    void Start()
    {
        _RB = GetComponent<Rigidbody>();
        _NM = GetComponent<NavMeshAgent>();
        _NM.speed += Random.Range(_NM.speed / (-10), _NM.speed / 10);

        _anim = GetComponent<Animator>();

        footStepsEmission = footSteps.emission;

    }

    void FixedUpdate()
    {
        /*_Target.y = 0;
        Vector3 dir = _Target.normalized;
        _RB.velocity = dir * _speed;*/
        if (_NM.hasPath)
        {
            footStepsEmission.enabled = true;
            _anim.SetBool("IsRunning", true);
        }
        else
        {
            footStepsEmission.enabled = false;
            _anim.SetBool("IsRunning", false);
        }
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

    // event subscribe example
    private void OnEnable()
    {
        enemyHealth.death += UnParentFootSpets;
    }
    private void OnDisable()
    {
        enemyHealth.death -= UnParentFootSpets;
    }
    //

    public void UnParentFootSpets()
    {
        this.enabled = false;
        footStepsEmission.enabled = false;
        Destroy(footSteps.gameObject , footSteps.startLifetime);
        footSteps.transform.parent = null;
    }
}
