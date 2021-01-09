using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{

    [Header("Data")]
    [SerializeField] private DoorSetActive[] _doors;
    [SerializeField] private GameObject _enemies;
    [SerializeField] private GameObject SpawnEffect;
 
    [Header("Enemies")]
    [SerializeField] private GameObject[] _objToSpawn;
    [SerializeField] private int[] _spawnChanceTable;
    private int _spawnChanceTotal;

    [Header("Spawn Range")]
    [SerializeField] private float _xSpawnRange;
    [SerializeField] private float _ySpawn = 0.5f;
    [SerializeField] private float _zSpawnRange;

    [Header("Spawn Mods")]
    [SerializeField] private float _spawnRate;
    [SerializeField] private int _maxEnemiesCount;
    private int _enemiesCount;

    private bool IsClear;

    private float speed;
    //private bool IsPlayerInside;

    [SerializeField] private float _aiEnableDelay;

    private void Awake()
    {
        foreach(int weight in _spawnChanceTable)
        {
            _spawnChanceTotal += weight;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void CloseAllDoors()
    {
        for (int i = 0 ; i < _doors.Length ; i++ )
        {
            _doors[i].CloseDoor();
        }
    }

    private void OpenAllDoors()
    {
        for (int i = 0; i < _doors.Length; i++)
        {
            _doors[i].OpenDoor();
        }
    }

    private void SpawnEnemy()
    {
        int RandomPrefab = 0;

        int randomNumber = Random.Range(0 , _spawnChanceTotal);

        for (int i = 0; i < _spawnChanceTable.Length ; i++)
        {
            if (randomNumber <= _spawnChanceTable[i])
            {
                RandomPrefab = i;
                break;
            }
            else
            {
                randomNumber -= _spawnChanceTable[i];
            }
        }
       /* switch (_objToSpawn.Length)
        {
            case 1:
                RandomPrefab = 0;
                break;
            case 2:
                if (Random.value >= SpawnChance[0]) RandomPrefab = 0;
                if (Random.value < SpawnChance[1]) RandomPrefab = 1;
                break;
            case 3:
                if (Random.value >= SpawnChance[0]) RandomPrefab = 0;
                if (Random.value < SpawnChance[1]) RandomPrefab = 1;
                if (Random.value < SpawnChance[2]) RandomPrefab = 2;
                break;
        }*/

        Vector3 randomPosition = new Vector3(Random.Range(-_xSpawnRange , _xSpawnRange) , _ySpawn , Random.Range(-_zSpawnRange , _zSpawnRange));
        GameObject Enemy = Instantiate(_objToSpawn[RandomPrefab], _enemies.transform.position + randomPosition , Quaternion.identity, _enemies.transform);
        Instantiate(SpawnEffect, Enemy.transform.position, SpawnEffect.transform.rotation);

        Vector3 startScale = gameObject.transform.localScale;
        Enemy.transform.localScale = Vector3.zero;
        LeanTween.scale(Enemy , startScale , 0.5f ).setEaseOutExpo();

        EnemyCombat EC;
        if (EC = Enemy.GetComponent<EnemyCombat>())
        {
            EC.PauseCombatAI(_aiEnableDelay);
        }

        TurretCombat TC;
        if (TC = Enemy.GetComponent<TurretCombat>())
        {
            TC.PauseCombatAI(_aiEnableDelay);
        }

    }

    IEnumerator Spawn()
    {
        while (_enemiesCount < _maxEnemiesCount)
        {
            SpawnEnemy();
            _enemiesCount++;
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    IEnumerator CheckRoom()
    {
        while (_enemies.transform.childCount > 0)
        {
            yield return new WaitForSeconds(1);
        }

        OpenAllDoors();
        IsClear = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !IsClear)
        {
            CloseAllDoors();
            //IsPlayerInside = true;

            StartCoroutine(Spawn());

            StartCoroutine(CheckRoom());
        }
    }
    private void TurretShoot()
    {
      
    }
}
