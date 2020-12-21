using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    [SerializeField] private GameObject _damageEffect;

    void Start()
    {
        _currentHealth = _maxHealth;
    }
    void Update()
    {
        
    }
    public void GetDamage(int damage , Vector3 hitPoint)
    {
        _currentHealth -= damage;

        Vector3 directionToDamage = hitPoint - transform.position;
        Quaternion damageRotation = Quaternion.FromToRotation(transform.forward, directionToDamage);
        Instantiate(_damageEffect, transform.position, damageRotation);

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    public void GetHeal(int heal)
    {
        if (_currentHealth + heal > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        else
        {
            _currentHealth += heal;
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
