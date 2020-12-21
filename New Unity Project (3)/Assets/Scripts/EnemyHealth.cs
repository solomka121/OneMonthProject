using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    [SerializeField] private GameObject _damageEffect;
    [SerializeField] private Color _damageColor;
    private Color _startColor;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _startColor = GetComponent<MeshRenderer>().material.color;
    }
    private void Update()
    {

    }

    public void GetDamage(int damage , Vector3 hitPoint)
    {
        _currentHealth -= damage;

        Vector3 directionToDamage = hitPoint - transform.position;
        Quaternion damageRotation = Quaternion.FromToRotation(transform.forward, directionToDamage);
        Instantiate(_damageEffect, transform.position, damageRotation);

        LeanTween.color(gameObject, _damageColor, 0.05f).setOnComplete(DamageBackToNormal);

        if (_currentHealth <= 0) Death();
    }
    private void DamageBackToNormal()
    {
        LeanTween.color(gameObject, _startColor, 0.1f);
    }
    private void Death()
    {
        LeanTween.scale(gameObject, Vector3.zero , 0.5f).setEaseOutExpo();
        Destroy(gameObject , 0.5f);
    }
}
