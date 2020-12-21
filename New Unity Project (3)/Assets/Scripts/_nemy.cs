using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _target;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _visibility;
    [SerializeField] private Transform[] _moveSpots;

    [SerializeField] private float _speed;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _startWaitTime = 3;

    private float _startOffset = 0.5f;
    private int _randomSpot;

    #endregion


    #region UnityMethods

    private void Start()
    {

        _target = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<NavMeshAgent>();

        _waitTime = _startWaitTime;
        _randomSpot = Random.Range(0, _moveSpots.Length);
    }

    private void Update()
    {
        Patrol();
        GetComponent<NavMeshAgent>().SetDestination(_target.position);
    }

    private void FixedUpdate()
    {
        var color = Color.red;
        RaycastHit hit;

        var startRaycastPosition = CalculateOffSet(transform.position);
        var directionToPlayer = CalculateOffSet(_target.position) - startRaycastPosition;
        var rayCast = Physics.Raycast(startRaycastPosition, directionToPlayer, out hit, _visibility, _mask);

        if (rayCast)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Chase();
                color = Color.green;
            }
        }
        var dir = directionToPlayer.normalized * _visibility;
        Debug.DrawRay(startRaycastPosition, directionToPlayer, color);
    }

    #endregion


    #region Methods

    private Vector3 CalculateOffSet(Vector3 position)
    {
        position.y += _startOffset;
        return position;
    }

    private void Patrol() 
    {
        transform.position = Vector3.MoveTowards(transform.position, _moveSpots[_randomSpot].position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _moveSpots[_randomSpot].position) < 0.2f)
        {
            if (_waitTime <= 0)
            {
                _randomSpot = Random.Range(0, _moveSpots.Length);
                _waitTime = _startWaitTime;
            }
            else
            {
                _waitTime -= Time.deltaTime;
            }
        }
    }

    private void Chase() 
    {
        transform.LookAt(_target);
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    #endregion
}
