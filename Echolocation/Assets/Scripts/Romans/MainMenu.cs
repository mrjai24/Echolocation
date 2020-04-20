using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string firstLevel;
    [SerializeField]
    private string savedLevel;
    [SerializeField]
    private string options;
    [SerializeField]
    private string credits;
    [SerializeField]
    private string shoeMenu;

    [SerializeField]
    private GameObject continueButton;

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        gameManager.LoadData();
        string level = gameManager.nextLevel;

        if (level.Length == 0)
        {
            continueButton.SetActive(false);
        }
        else
        {
            continueButton.SetActive(true);
            savedLevel = level;
        }
    }



    public void ContinueGame()
    {
        SceneManager.LoadScene(savedLevel);
    }

    public void PlayGame()
    {
        gameManager.nextLevel = "";
        gameManager.SaveData();
        SceneManager.LoadScene(firstLevel);
    }

    public void OpenShoeMenu()
    {
        SceneManager.LoadScene(shoeMenu);
    }

    public void OpenOptions()
    {
        gameManager.LoadData();
        SceneManager.LoadScene(options);
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene(credits);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
