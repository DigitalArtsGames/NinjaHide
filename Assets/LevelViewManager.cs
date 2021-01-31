using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelViewManager : MonoBehaviour
{
    [SerializeField] private int viewLevelsCount;
    [SerializeField] private LevelComponent levelPrefab;
    [SerializeField] private LevelManager levelManager;


    private List<LevelComponent> levelComponents;

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
                levelPrefab.SetText((j + 1).ToString());
                levelComponents.Add(levelPrefab);
                
                Instantiate(levelPrefab, transform);
            }
        }
    }

}
