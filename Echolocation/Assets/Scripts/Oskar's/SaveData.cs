using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
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

    public SaveData (GameManager gm)
    {
        currentLevel = gm.currentLevel;
        nextLevel = gm.nextLevel;
        soundVolume = gm.soundVolume;
        fullScreen = gm.fullScreen;
        screenWidth = gm.screenWidth;
        screenHeight = gm.screenHeight;
        screenRefreshRate = gm.screenRefreshRate;
        //currentShoe = gm.currentShoe;
        //unlockedShoes = gm.unlockedShoes;
    }

}
