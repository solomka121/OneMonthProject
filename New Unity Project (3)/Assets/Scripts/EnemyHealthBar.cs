using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider _helathBar;
    public Image Fill;
    public Color stunFillColor;
    private Color _fillStartColor;

    [SerializeField] private bool isFadingIn;
    [SerializeField] private bool isFadingOut;

    // Start is called before the first frame update
    void Start()
    {
        _fillStartColor = Fill.color;
    }

    public void IsStunned(bool state)
    {
        if (state)
        {
            Fill.color = stunFillColor;
            //if(!isFadingIn) StartCoroutine(FillFadeIn());
        }
        else
        {
            Fill.color = _fillStartColor;
            //if(!isFadingOut) StartCoroutine(FillFadeOut());
        }
    }

/*    private IEnumerator FillFadeIn()
    {
        isFadingIn = true;
        while (Fill.color != stunFillColor)
        {
            float step = 0.1f;
            Fill.color = Color.Lerp(Fill.color, stunFillColor , step);
            step *= 2f;
            yield return new WaitForEndOfFrame();
        }
        isFadingIn = false;
        StopCoroutine(FillFadeIn());
    }

    private IEnumerator FillFadeOut()
    {
        isFadingOut = true;
        while (Fill.color != _fillStartColor)
        {
            float step = 0.1f;
            Fill.color = Color.Lerp(Fill.color, _fillStartColor, step);
            step *= 2f;
            yield return new WaitForEndOfFrame();
        }
        isFadingOut = false;
        StopCoroutine(FillFadeOut());
    }*/

    public void SetMaxHealth(int maxHealth)
    {
        _helathBar.maxValue = maxHealth;
    }

    public void UpdateHealth(int currentHealth)
    {
        _helathBar.value = currentHealth;
    }
}
