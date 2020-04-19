using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField]
    private string mainMenu;

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
