using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadingController : MonoBehaviour
{
    public Image Foreground;
    public float FadingStep;
    public Color FadingInColor;
    public Color FadingOutColor;

    public PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadingOut());
    }
    private void Awake()
    {
        Foreground.enabled = true;
    }

    private void FadeIn()
    {
        StartCoroutine(FadingIn());
    }

    IEnumerator FadingOut()
    {
        float fadingStep = FadingStep;
        while (Foreground.color != FadingOutColor)
        {
            Color currentColor = Color.Lerp(Foreground.color, FadingOutColor, fadingStep);
            fadingStep *= 1.1f;
            Foreground.color = currentColor;

            yield return new WaitForFixedUpdate();
        }

        Foreground.enabled = false;
        StopCoroutine(FadingOut());
    }

    private void OnEnable()
    {
        playerHealth.death += FadeIn;
    }
    private void OnDisable()
    {
        playerHealth.death -= FadeIn;
    }

    IEnumerator FadingIn()
    {
        Foreground.enabled = true;
        float fadingStep = FadingStep;

        while (Foreground.color != FadingInColor)
        {
            Color currentColor = Color.Lerp(Foreground.color, FadingInColor, fadingStep);
            fadingStep *= 1.05f;
            Foreground.color = currentColor;

            yield return new WaitForFixedUpdate();
        }

        StopCoroutine(FadingOut());
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
}
