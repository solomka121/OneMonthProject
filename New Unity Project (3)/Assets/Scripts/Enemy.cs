using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody _RB;
    [SerializeField] private int MaxHealth;
    [SerializeField] private int CurrentHealth;

    private void Start()
    {
        _RB = GetComponent<Rigidbody>();
        CurrentHealth = MaxHealth;
    }
    private void FixedUpdate()
    {
        _RB.velocity = transform.right;
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
