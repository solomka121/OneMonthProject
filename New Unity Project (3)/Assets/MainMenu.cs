using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public Canvas BlockUi_5; // _5 = sort order is 5

    [Header("Main")]
    public GameObject Menu;
    public float Menu_TransitionDuration;
    public Button Menu_Play;
    public Button Menu_Quit;
    public Button Menu_Settings;
    public Button Menu_LeaderBoard;
    public Button Menu_Account;

    [Header("Settings")]
    public GameObject SettingsMenu;
    public float SettingsMenu_TransitionDuration;
    public Button SettingsMenu_Back;

    public Image Foreground;
    public float FadingStep;
    public Color FadingInColor;
    public Color FadingOutColor;

    void Awake()
    {
        Menu_Play.onClick.AddListener(Menu_PlayEvent);
        Menu_Quit.onClick.AddListener(Menu_QuitEvent);
        Menu_Settings.onClick.AddListener(Menu_SettingsEvent);
        Menu_LeaderBoard.onClick.AddListener(Menu_LeaderBoardEvent);
        Menu_Account.onClick.AddListener(Menu_AccountEvent);

        SettingsMenu_Back.onClick.AddListener(SettingsMenu_BackEvent);
    }

    public void Menu_PlayEvent()
    {
        LeanTween.scale(Menu_Play.gameObject , Menu_Play.gameObject.transform.localScale * 4, 2f).setEaseOutBack();
        Foreground.enabled = true;
        StartCoroutine(FadingIn());
    }

    IEnumerator FadingIn()
    {
        while (Foreground.color != FadingInColor)
        {
            Color currentColor = Color.Lerp(Foreground.color, FadingInColor, FadingStep);
            FadingStep *= 1.1f;
            Foreground.color = currentColor;

            yield return new WaitForFixedUpdate();
        }


        // on complete fading in
        SceneManager.LoadScene(1);
    }

    public void Menu_QuitEvent()
    {
        Application.Quit();
    }
    public void Menu_SettingsEvent()
    {
        BlockUi_5.enabled = true;

        SettingsMenu.transform.localScale = Vector3.zero;

        SettingsMenu.SetActive(true);
        LeanTween.scale(SettingsMenu , Vector3.one , SettingsMenu_TransitionDuration).setEaseOutExpo();
        LeanTween.scale(Menu, Vector3.zero, Menu_TransitionDuration).setOnComplete(Menu_Disable).setEaseOutExpo();
    }

    private void Menu_Disable()
    {
        Menu.SetActive(false);
        BlockUi_5.enabled = false;
    }

    public void Menu_LeaderBoardEvent()
    {
        // TO DO
    }
    public void Menu_AccountEvent()
    {
        // TO DO
    }

    public void SettingsMenu_BackEvent()
    {
        BlockUi_5.enabled = true;
        Menu.transform.localScale = Vector3.zero;

        Menu.SetActive(true);
        LeanTween.scale(Menu, Vector3.one, Menu_TransitionDuration).setEaseOutExpo();
        LeanTween.scale(SettingsMenu, Vector3.zero, SettingsMenu_TransitionDuration).setOnComplete(SettingsMenu_Disable).setEaseOutExpo();
    }

    private void SettingsMenu_Disable()
    {
        SettingsMenu.SetActive(false);
        BlockUi_5.enabled = false;
    }
}
