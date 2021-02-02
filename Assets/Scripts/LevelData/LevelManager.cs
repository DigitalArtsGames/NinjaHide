﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public LevelData levelData;

    public GameObject timeLine; 
    public int currentLevelIndex = 0;
    public Room currentLevel;
    //public GameObject currentLevel;
    public GameObject rewardMenu;
    //public StarsManager starsContainer;

    private void Start()
    {
        currentLevelIndex = 0;
    }

    public void LoadNextLevel()
    {
        if(levelData.rooms.Length != currentLevelIndex)
        {
            if(currentLevel != null)
            {
                Destroy(currentLevel.gameObject);
            }
            //currentLevelIndex++;
            currentLevel = Instantiate(levelData.rooms[currentLevelIndex]);
        } else
        {
            Debug.Log("THE END!");
        }
    }

    public void LoadCurrentLevel()
    {
        if (levelData.rooms.Length != currentLevelIndex)
        {   
            if (currentLevel != null)
            {
                Destroy(currentLevel.gameObject);
            }
            currentLevel = Instantiate(levelData.rooms[currentLevelIndex]);
        }
    }

    public void LoadCurrentLevelReplay()
    {
        if (levelData.rooms.Length != currentLevelIndex)
        {
            if (currentLevel != null)
            {
                Destroy(currentLevel.gameObject);
            }
            currentLevelIndex--;
            currentLevel = Instantiate(levelData.rooms[currentLevelIndex]);
        }
    }

    public void ExitGame()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        Destroy(currentLevel.gameObject);
        Time.timeScale = 1f;
    }

    //public void LoadRewardMenu()
    //{
    //    rewardMenu.SetActive(true);
    //    starsContainer.CollectStars(3);
    //}

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }

    //public void ReloadScene()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}
}
