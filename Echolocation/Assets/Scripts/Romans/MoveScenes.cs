﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScenes : MonoBehaviour
{
    [SerializeField]
    private string nextLevel;
    [SerializeField] 
    private int bellFlowerNeeded;

    private void OnTriggerEnter2D(Collider2D col)
    {
        int bellFlowers = col.GetComponent<PickUp>().bellFlowers;
        if (col.gameObject.CompareTag("Player") && bellFlowers == bellFlowerNeeded) 
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}