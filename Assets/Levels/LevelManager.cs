using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //[SerializeField] public LevelData levelData;

    public LevelsCollection levelsCollection;

    public GameObject timeLine; 
    public int currentLevelIndex = 0;
    public LevelData currentLevel;
    //public GameObject currentLevel;
    public GameObject rewardMenu;
    //public StarsManager starsContainer;

    private void Start()
    {
        currentLevelIndex = 0;
    }

    public void LoadNextLevel()
    {
        if(levelsCollection.levels.Length != currentLevelIndex)
        {
            if(currentLevel != null)
            {
                Destroy(currentLevel);
            }
            //currentLevelIndex++;
            currentLevel = Instantiate(levelsCollection.levels[currentLevelIndex]);
        } else
        {
            Debug.Log("THE END!");
        }
    }

    public void LoadCurrentLevel()
    {
        if (levelsCollection.levels.Length != currentLevelIndex)
        {   
            if (currentLevel != null)
            {
                Destroy(currentLevel);
            }
            currentLevel = Instantiate(levelsCollection.levels[currentLevelIndex]);
        }
    }

    public void LoadCurrentLevelReplay()
    {
        if (levelsCollection.levels.Length != currentLevelIndex)
        {
            if (currentLevel != null)
            {
                Destroy(currentLevel);
            }
            currentLevelIndex--;
            currentLevel = Instantiate(levelsCollection.levels[currentLevelIndex]);
        }
    }

    public void ExitGame()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        Destroy(currentLevel);
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
