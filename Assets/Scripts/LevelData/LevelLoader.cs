using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] public LevelData levelData;
    
    public int currentLevelIndex = 0;
    public Room currentLevel;

    private void Start()
    {
        currentLevelIndex = 0;
        currentLevel = Instantiate(levelData.rooms[currentLevelIndex]);
    }

    public void LoadNextLevel()
    {
        if(levelData.rooms.Length >= currentLevelIndex + 1)
        {
            Destroy(currentLevel.gameObject);
            currentLevel = Instantiate(levelData.rooms[currentLevelIndex + 1]);
        }
    }
}
