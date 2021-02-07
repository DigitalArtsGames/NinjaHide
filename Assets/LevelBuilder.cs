using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public LevelManager levelManager;
    public SplineManager splineManager;
    public PlayerSpawner playerSpawner;

    private LevelData currentLevel;

    public delegate void SetSplineDelegate();
    public event SetSplineDelegate setSplineEvent;
    
    public delegate void SpawnRoomDelegate();
    public event SpawnRoomDelegate spawnRoomDelegate;

    
    public void Update()
    {
        UpdateCurrentLevel();
    }

    public void UpdateCurrentLevel()
    {
        if (currentLevel == null && levelManager.currentLevel != null)
        {
            currentLevel = levelManager.currentLevel;
            spawnRoomDelegate();
            setSplineEvent();
        }
    }

    private void Start()
    {
        spawnRoomDelegate += SpawnPlatforms;
        setSplineEvent += SetVariables;
    }

    public void SetVariables()
    {
        SetSplines();
        SetPlayer();
    }

    public void SetSplines()
    {
        splineManager.splines = new BezierSpline[currentLevel.rooms.Length];
        for (int i = 0; i < currentLevel.rooms.Length; i++)
            splineManager.splines[i] = currentLevel.rooms[i].playerSpline;
        splineManager.enabled = true;
    }

    public void SetPlayer()
    {
        playerSpawner.SetSplineManager(splineManager);
        playerSpawner.SpawnPlayer();
        splineManager.playerSplineWalker = playerSpawner.GetSplineWalker();
        playerSpawner.SetSplineWalkerActive(true);
    }

    public void SpawnPlatforms()
    {
        for (int i = 0; i < currentLevel.rooms.Length; i++)
        {
            currentLevel.rooms[i] = Instantiate(currentLevel.rooms[i]);
        }
    }
}
