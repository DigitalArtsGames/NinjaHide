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
        //print(splineManager.currentSplineIndex);
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
        setSplineEvent -= SetVariables;
    }

    public void SetSplines()
    {
        //splineManager = new SplineManager();
        splineManager.splines = new BezierSpline[currentLevel.rooms.Length];
        for (int i = 0; i < currentLevel.rooms.Length; i++)
            splineManager.splines[i] = currentLevel.rooms[i].playerSpline;
        //if (splineManager.currentSplineIndex != 0)
        //{
        //    splineManager.currentSplineIndex = -1;


        //}
        //splineManager.SetCurrentSplineByIndex(splineManager.currentSplineIndex);
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
