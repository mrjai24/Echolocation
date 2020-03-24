using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField]
    Text countdown;

    float startingTime = 600f; // Seconds

    private void Update()
    {
        startingTime -= Time.deltaTime;

        int seconds = (int)(startingTime % 60);
        int minutes = (int)(startingTime / 60) % 60;

        string timer = string.Format("{0:00}:{1:00}", minutes, seconds);

        countdown.text = timer;
    }
}
