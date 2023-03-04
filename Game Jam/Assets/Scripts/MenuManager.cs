using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject loadingPanel;
    [SerializeField] GameObject pauseMenu;
    public bool shouldLoad = false;
    public bool shouldPause = false;
    bool isPaused = false;

    private void Update()
    {
        if (shouldPause && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                Pause();
            else
                Resume();
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("The Lab");
    }
    public void Levels()
    {
        SceneManager.LoadScene("Levels");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Menu()
    {
        if (shouldLoad)
        {
            loadingPanel.SetActive(true);
        }
        SceneManager.LoadScene("Menu");
    }
    public void LoadLevel(int level)
    {
        loadingPanel.SetActive(true);
        SceneManager.LoadScene(level);
    }
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
