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

    public void Update()
    {
        UpdateCurrentLevel();
    }

    public void UpdateCurrentLevel()
    {
        if (currentLevel == null && levelManager.currentLevel != null)
        {
            currentLevel = levelManager.currentLevel;
            setSplineEvent();
        }
    }

    private void Start()
    {
        setSplineEvent += SetVariables;
    }

    public void SetVariables()
    {
        SetSplines();
        SetPlayer();
    }

    public void SetSplines()
    {
        splineManager.splines = new BezierSpline[3];
        for (int i = 0; i < currentLevel.rooms.Length; i++)
            splineManager.splines[i] = currentLevel.rooms[i].playerSpline;
        splineManager.enabled = true;
    }

    public void SetPlayer()
    {
        playerSpawner.SetSplineManager(splineManager);
        splineManager.playerSplineWalker = playerSpawner.GetSplineWalker();
        playerSpawner.SpawnPlayer();
        playerSpawner.SetSplineWalkerActive(true);
    }
}
