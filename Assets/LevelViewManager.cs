using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelViewManager : MonoBehaviour
{
    [SerializeField] private int viewLevelsCount;
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private LevelManager levelManager;

    void Start()
    {
        DrawLevels();
    }


    public void DrawLevels()
    {

        for (int j = 0; j < levelManager.levelData.rooms.Length; j++)
        {
            if(levelManager.levelData.rooms.Length <= viewLevelsCount)
            {
                levelPrefab.GetComponent<LevelComponent>().SetText((j + 1).ToString());
                
                Instantiate(levelPrefab, transform);
            }
        }
    }

}
