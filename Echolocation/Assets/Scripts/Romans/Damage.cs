using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Damage : MonoBehaviour
{
    [SerializeField]
    private string thisLevel;

    private void Start()
    {
        thisLevel = SceneManager.GetActiveScene().name;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Killed");
        if (col.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(thisLevel);
        }
    }
}
