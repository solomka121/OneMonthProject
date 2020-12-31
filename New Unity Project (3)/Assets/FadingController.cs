using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingController : MonoBehaviour
{
    public Image Foreground;
    public float FadingStep;
    public Color FadingInColor;
    public Color FadingOutColor;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadingOut());
    }
    private void Awake()
    {
        Foreground.enabled = true;
    }
    IEnumerator FadingOut()
    {
        while (Foreground.color != FadingOutColor)
        {
            Color currentColor = Color.Lerp(Foreground.color, FadingOutColor, FadingStep);
            FadingStep *= 1.1f;
            Foreground.color = currentColor;

            yield return new WaitForFixedUpdate();
        }

        Foreground.enabled = false;
        StopCoroutine(FadingOut());
    }
}
