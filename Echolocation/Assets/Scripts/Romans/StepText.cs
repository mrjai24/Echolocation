using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepText : MonoBehaviour
{
    [SerializeField]
    Text stepsTakenText;

    PlayerMovement playerMov;

    string stepsTaken;

    private void Start()
    {
        GameObject Player = GameObject.Find("Player");
        playerMov = Player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        stepsTaken = playerMov.steps.ToString();
        stepsTakenText.text = stepsTaken;
    }
}
