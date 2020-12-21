using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    [SerializeField] private GameObject _damageEffect;

    Vector3 tt = Vector3.zero;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }
    private void Update()
    {
        Debug.DrawLine(Vector3.zero , tt);
    }

    public void GetDamage(int damage , Vector3 hitPoint)
    {
        _currentHealth -= damage;
        tt = hitPoint;
        Instantiate(_damageEffect, transform.position, Quaternion.identity);
        if (_currentHealth <= 0) Death();
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
