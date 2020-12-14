﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
    }
    void Update()
    {
        
    }
    public void GetDamage(int damage)
    {
        _currentHealth -= damage;
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
