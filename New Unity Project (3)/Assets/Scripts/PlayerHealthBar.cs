using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider _helathBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetMaxHealth(int maxHealth)
    {
        _helathBar.maxValue = maxHealth;
    }

    public void UpdateHealth(int currentHealth)
    {
        _helathBar.value = currentHealth;
    }
}
