using System.Collections;
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

    private void Start()
    {
        LoadData();
    }
    public void SaveData()
    {
        SaveManager.SaveData(this);
    }

    public void LoadData()
    {
        SaveData data = SaveManager.LoadData();
        if(data !=null)
        {
            currentLevel = data.currentLevel;
            nextLevel = data.nextLevel;
            soundVolume = data.soundVolume;
            fullScreen = data.fullScreen;
            screenWidth = data.screenWidth;
            screenHeight = data.screenHeight;
            screenRefreshRate = data.screenRefreshRate;
        }
    }


}
