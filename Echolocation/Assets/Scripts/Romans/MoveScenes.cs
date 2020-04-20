using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScenes : MonoBehaviour
{
    private static bool isGamePaused = false;

    public GameObject gameStatusUI;
    public GameObject winScreen;

    [SerializeField]
    private string nextLevel;
    [SerializeField] 
    private int bellFlowerNeeded;
    [SerializeField]
    private string mainMenu;

    public AudioClip WinSound;

    public void Resume()
    {
        winScreen.SetActive(false);
        gameStatusUI.SetActive(true);
        Time.timeScale = 1f; // Restart time in game
        isGamePaused = false;
    }

    private void Pause()
    {
        winScreen.SetActive(true);
        gameStatusUI.SetActive(false);
        Time.timeScale = 0f; // Stop time in game
        isGamePaused = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        int bellFlowers = col.GetComponent<PickUp>().bellFlowers;
        if (col.gameObject.CompareTag("Player") && bellFlowers == bellFlowerNeeded) 
        {
            SoundManager.PlaySound(WinSound);
            Pause();
        }
    }

    public void nextLevelButton()
    {
        Resume();
        SceneManager.LoadScene(nextLevel);
    }

    public void ExitToMainMenu()
    {
        Resume();
        SceneManager.LoadScene(mainMenu);
    }
}
