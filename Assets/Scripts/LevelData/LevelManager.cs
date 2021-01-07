using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public LevelData levelData;

    public GameObject timeLine; 
    public int currentLevelIndex = 0;
    public Room currentLevel;

    private void Start()
    {
        currentLevelIndex = 0;
        //if(3 > 5)
        //{
        //    currentLevel = Instantiate(levelData.rooms[currentLevelIndex]);
        //}
    }

    public void LoadNextLevel()
    {
        //timeLine.SetActive(true);
        if(levelData.rooms.Length != currentLevelIndex)
        {
            if(currentLevel != null)
            {
                Destroy(currentLevel.gameObject);
            }
            currentLevel = Instantiate(levelData.rooms[currentLevelIndex]);
            currentLevelIndex++;
        } else
        {
            Debug.Log("THE END!");
        }
    }
}
