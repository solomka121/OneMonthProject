using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    [SerializeField] private GameObject _damageEffect;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }
    private void Update()
    {
       
    }

    public void GetDamage(int damage , Vector3 hitPoint)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) Death();
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
