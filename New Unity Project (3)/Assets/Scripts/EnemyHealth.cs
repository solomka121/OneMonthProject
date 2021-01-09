using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int stunResistance;

    public GameObject body;
    public EnemyHealthBar enemyHealthBar;

    // example of event (is it correct here ?) 
    public delegate void OnDeath();
    public event OnDeath death;
    // 

    // stun event
    public delegate void OnStun(bool state);
    public event OnStun stun;
    //

    private bool IsStunned;

    [SerializeField] private GameObject _damageEffect;
    [SerializeField] private Color _damageColor;
    private Color _bodyColor;

    private void Start()
    {
        _currentHealth = _maxHealth;
        enemyHealthBar.SetMaxHealth(_maxHealth);
        enemyHealthBar.UpdateHealth(_currentHealth);

        _bodyColor = body.GetComponent<MeshRenderer>().material.color;
    }
    private void Update()
    {

    }

    //get damage
    public void GetDamage(int damage , Vector3 hitPoint)
    {
        _currentHealth -= damage;
        enemyHealthBar.UpdateHealth(_currentHealth);

        Vector3 directionToDamage = hitPoint - transform.position;
        Quaternion damageRotation = Quaternion.FromToRotation(transform.forward, directionToDamage);
        Instantiate(_damageEffect, transform.position, damageRotation);

        LeanTween.color(body, _damageColor, 0.05f).setOnComplete(DamageBackToNormal);

        if (_currentHealth <= 0) Death();
    }

    // get damage with stun chance
    public void GetDamage(int damage, Vector3 hitPoint , float stunChance , float stunDuration)
    {
        _currentHealth -= damage;
        enemyHealthBar.UpdateHealth(_currentHealth);

        stunChance -= stunChance / 100 * stunResistance;
        if (stunChance > 0 && !IsStunned)
        {
            if (Random.value < stunChance)
            {
                if (stun != null) stun(true);
                StartCoroutine(StunTimer(stunDuration));
                enemyHealthBar.IsStunned(true);
            }
        }

        Vector3 directionToDamage = hitPoint - transform.position;
        Quaternion damageRotation = Quaternion.FromToRotation(transform.forward, directionToDamage);
        Instantiate(_damageEffect, transform.position, damageRotation);

        LeanTween.color(body, _damageColor, 0.05f).setOnComplete(DamageBackToNormal);

        if (_currentHealth <= 0) Death();
    }

    IEnumerator StunTimer(float time)
    {
        IsStunned = true;
        yield return new WaitForSeconds(time);
        if (stun != null) stun(false);
        enemyHealthBar.IsStunned(false);
        IsStunned = false;

    }

    private void DamageBackToNormal()
    {
        LeanTween.color(body, _bodyColor, 0.1f);
    }

    private void Death()
    {
        if (death != null) death();
        LeanTween.scale(gameObject, Vector3.zero , 0.5f).setEaseOutExpo();
        Destroy(gameObject , 0.5f);
    }
}
