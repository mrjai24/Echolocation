using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Damage : MonoBehaviour
{
    public AudioClip deathSound;


    private static bool isGamePaused = false;

    public GameObject gameStatusUI;
    public GameObject loseScreen;

    [SerializeField]
    private string thisLevel;
    [SerializeField]
    private string mainMenu = "Main Menu";

    public void Resume()
    {
        loseScreen.SetActive(false);
        gameStatusUI.SetActive(true);
        Time.timeScale = 1f; // Restart time in game
        isGamePaused = false;
    }

    private void Pause()
    {
        loseScreen.SetActive(true);
        gameStatusUI.SetActive(false);
        Time.timeScale = 0f; // Stop time in game
        isGamePaused = true;
    }


    private void Start()
    {
        thisLevel = SceneManager.GetActiveScene().name;
        GameObject gui = GameObject.Find("GUI");
        gameStatusUI = gui.transform.Find("GameStatusUI").gameObject;
        loseScreen = gui.transform.Find("LoseScreen").gameObject;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Killed");
        if (col.gameObject.CompareTag("Player"))
        {
            SoundManager.PlaySound(deathSound);
            Pause();
        }
    }

    public void tryAgainButton()
    {
        Resume();
        SceneManager.LoadScene(thisLevel);
    }

    public void ExitToMainMenu()
    {
        Resume();
        SceneManager.LoadScene(mainMenu);
    }
}
