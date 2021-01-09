using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    public PlayerHealthBar playerHealthBar;
    public GameObject _damageEffect;

    // example of event
    public delegate void OnDeath();
    public event OnDeath death;
    // 

    void Start()
    {
        _currentHealth = _maxHealth;
        playerHealthBar.SetMaxHealth(_maxHealth);
        playerHealthBar.UpdateHealth(_currentHealth);
    }
    void Update()
    {
        
    }
    public void GetDamage(int damage , Vector3 hitPoint)
    {
        _currentHealth -= damage;

        playerHealthBar.UpdateHealth(_currentHealth);

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
        playerHealthBar.UpdateHealth(_currentHealth);
    }

    private void Death()
    {
        death();
        Time.timeScale = 0.5f;

        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
    }
}
