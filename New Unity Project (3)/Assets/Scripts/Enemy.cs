using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int MaxHealth;
    [SerializeField] private int CurrentHealth;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void GetDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0) Death();
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
