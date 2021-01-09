using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    //[Header("Pause Menu")]
    [SerializeField] private GameObject _pauseMenu;
    private Vector3 _pauseMenuPos;
    private Vector3 _pauseMenuExpoPos;
    private bool _isPaused;

    [SerializeField] private Button _pauseMenuResume;
    [SerializeField] private Button _pauseMenuSettings;
    [SerializeField] private Button _pauseMenuMenu;
    [SerializeField] private Button _pauseMenuExit;

    private bool _isAnimating;

    private void Awake()
    {
        _pauseMenuPos = _pauseMenu.transform.position;
        _pauseMenuExpoPos = _pauseMenuPos + new Vector3(600, 0, 0);

        _pauseMenuResume.onClick.AddListener(Resume);
        _pauseMenuSettings.onClick.AddListener(Settings);
        _pauseMenuMenu.onClick.AddListener(Menu);
        _pauseMenuExit.onClick.AddListener(Exit);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Pause()
    {
        _isPaused = true;
        _isAnimating = true;

        Time.timeScale = 0;

        _pauseMenu.transform.position = _pauseMenuExpoPos;
        _pauseMenu.SetActive(true);

        LeanTween.moveX(_pauseMenu, _pauseMenuPos.x, .8f).setEaseOutExpo().setOnComplete(EndAnimation).setIgnoreTimeScale(true);
    }
    private void EndAnimation()
    {
        _isAnimating = false;
    }
    private void Resume()
    {
        _isPaused = false;
        _isAnimating = true;

        Time.timeScale = 1f;

        LeanTween.moveX(_pauseMenu, _pauseMenuExpoPos.x, .5f).setEaseOutExpo().setOnComplete(_pauseMenu_disable).setIgnoreTimeScale(true);
    }
    private void _pauseMenu_disable()
    {
        _pauseMenu.SetActive(false);
        _isAnimating = false;
    }
    private void Settings()
    {

    }
    private void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    private void Exit()
    {
        Application.Quit();
    }



    void Update()
    {

        if (Input.GetKeyDown("escape") && !_isAnimating)
        {
            if (_isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
}
