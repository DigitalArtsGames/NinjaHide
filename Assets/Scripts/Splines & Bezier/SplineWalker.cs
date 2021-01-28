﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{

    #region SplineWalker Settings
    [Header("SplineWalker Settings")]


    public BezierSpline spline;

    [SerializeField] private SplineWalkerMode mode;

    [SerializeField] private float nextPointTreshhold = 0.01f;

    [SerializeField] private SplineManager splineManager;

    public bool isNPC;

    public bool enableLookForward;
    #endregion

    #region HiddenVariables
    
    [HideInInspector] public static SplineWalker Instance;
    
    public event Action onSplineEnded;

    [HideInInspector] public int speed;

    [HideInInspector] public float progress;

    [HideInInspector] public float sliderProgress;

    [HideInInspector] public int currentIndex;

    [HideInInspector] public int currentProgressIndex;

    [HideInInspector] public List<Vector3> points;

    [HideInInspector] public int tempIndex;

    [HideInInspector] public bool isFinished;
    #endregion

    private void Start()
    {
        CheckInstance();
        points = spline.bezierPoints;
    }

    private void Update()
    {
        isFinished = IsFinishedPath();
        print(sliderProgress);
        progress = spline.GetProgress(currentIndex);
        if(!isNPC)
        {
            sliderProgress = spline.GetProgress(currentProgressIndex, splineManager.GetCommonFrequency());
        }
        if (points.Count > currentIndex)
        {
            if (Vector3.Distance(points[currentIndex], transform.position) < nextPointTreshhold)
            {
                GetNextPoint();
                if(sliderProgress != 1)
                {
                    currentProgressIndex++;
                }
            }

            var target = points[currentIndex];
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        if (enableLookForward)
        {
            Vector3 position = spline.GetPoint(progress);
            transform.LookAt(position + spline.GetDirection(progress));
        }
    }

    public Vector3 GetPlayerDirection()
    {
        Vector3 position = spline.GetPoint(progress);
        Vector3 dir = position + spline.GetDirection(progress);

        return dir;
    }

    public void SetPositionIndex(Vector3 position)
    {
        currentIndex = points.IndexOf(position);
        if (splineManager.currentSplineIndex > 0)
        {
            currentProgressIndex = splineManager.GetCommonIndex(position);
        }
        else
        {
            currentProgressIndex = points.IndexOf(position);
        }
    }
    private void GetNextPoint()
    {
        if (currentIndex + 1 >= points.Count)
        {
            if (mode == SplineWalkerMode.Once)
            {
                onSplineEnded?.Invoke();
                points = spline.bezierPoints;
                return;
            }
            if (mode == SplineWalkerMode.Loop)
            {
                progress = 0f;
                currentIndex = 0;
            }
        }
        currentIndex++;
    }

    public bool IsFinishedPath()
    {
        if(sliderProgress == 1)
        {
            return true;
        }
        return false;
    }

    public void UpdateTarget()
    {

    }

    public void SetSpeed(int speed)
    {
        this.speed = speed;
    }

    public void CheckInstance()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}


