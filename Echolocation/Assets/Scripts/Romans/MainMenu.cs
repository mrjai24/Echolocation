using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void ContinueGame()
    {
        SceneManager.LoadScene(savedLevel);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void OpenShoeMenu()
    {
        SceneManager.LoadScene(shoeMenu);
    }

    public void OpenOptions()
    {
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
