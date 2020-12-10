using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;

    [SerializeField] private float _spawnTime;
    private float _timeToSpawn;

    [SerializeField] private int maxEnemyCount;
    private int enemyCount;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < maxEnemyCount && _timeToSpawn <= 0)
        {
            Instantiate(_objectToSpawn, transform);
            _timeToSpawn = _spawnTime;
        }
        else
        {
            _timeToSpawn -= Time.deltaTime;
        }
    }
}
