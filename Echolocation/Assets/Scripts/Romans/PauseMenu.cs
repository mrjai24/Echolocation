using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool isGamePaused = false;
    public GameObject gameStatusUI;
    public GameObject pauseMenu;

    [SerializeField]
    private string restartLevel;
    [SerializeField]
    private string options;
    [SerializeField]
    private string shoeMenu;
    [SerializeField]
    private string mainMenu;

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Game is resumed");
        pauseMenu.SetActive(false);
        gameStatusUI.SetActive(true);
        Time.timeScale = 1f; // Restart time in game
        isGamePaused = false;
    }

    private void Pause()
    {
        Debug.Log("Game is paused");
        pauseMenu.SetActive(true);
        gameStatusUI.SetActive(false);
        Time.timeScale = 0f; // Stop time in game
        isGamePaused = true;
    }
    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(restartLevel);
        Resume();
    }
    public void OpenShoeMenu()
    {
        SceneManager.LoadScene(shoeMenu);
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene(options);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Resume();
    }
}
