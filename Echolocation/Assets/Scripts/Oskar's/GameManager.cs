﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string currentLevel;
    public string nextLevel;
    public float soundVolume;
    public bool fullScreen;
    public int screenWidth;
    public int screenHeight;
    public int screenRefreshRate;
    //public int currentShoe;
    //public bool[] unlockedShoes;


    private void Start()
    {
        //Debug.Log(Application.persistentDataPath + "/save.sav");
        LoadData();
    }
    public void SaveData()
    {
        SaveManager.SaveData(this);
    }

    public void LoadData()
    {
        SaveData data = SaveManager.LoadData();
        currentLevel = data.currentLevel;
        nextLevel = data.nextLevel;
        soundVolume = data.soundVolume;
        fullScreen = data.fullScreen;
        screenWidth = data.screenWidth;
        screenHeight = data.screenHeight;
        screenRefreshRate = data.screenRefreshRate;
        //currentShoe = data.currentShoe;
        //unlockedShoes = data.unlockedShoes;
    }

}