using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public LevelManager levelManager;
    public SplineManager splineManager;
    public PlayerSpawner playerSpawner;
    public RoomsSpawner roomsSpawner;

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
        if (levelManager.currentLevel != null)
        {
            if (currentLevel != levelManager.currentLevel)
            {
                ClearLevel();
                currentLevel = levelManager.currentLevel;
                spawnRoomDelegate();
                setSplineEvent();
            }
        }
    }

    private void Start()
    {
        currentLevel = levelManager.currentLevel;
        SpawnPlatforms();
        SetVariables();

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
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            playerSpawner.SpawnPlayer();
        }
        splineManager.playerSplineWalker = playerSpawner.GetSplineWalker();
        playerSpawner.SetSplineWalkerActive(true);
    }

    public void SpawnPlatforms()
    {
        roomsSpawner.SpawnRoomsRandomly(currentLevel.rooms);
    }

    public void ClearLevel()
    {
        roomsSpawner.ClearRooms(currentLevel.rooms);
    }
}
