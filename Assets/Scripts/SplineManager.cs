using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineManager : MonoBehaviour
{
    public SplineWalker playerSplineWalker;
    [SerializeField] private BezierSpline[] splines;

    public int currentSplineIndex;

    private void Start()
    {
        playerSplineWalker.onSplineEnded += OnCurrentSplineEnded;

        currentSplineIndex = 0;
        splines = GetComponentsInChildren<BezierSpline>();
        playerSplineWalker.spline = splines[currentSplineIndex];
    }

    private void OnCurrentSplineEnded()
    {
        if (GetNextSpline())
        {
            playerSplineWalker.currentIndex = 0;
            playerSplineWalker.spline = splines[currentSplineIndex];
        }
        else
        {
            //TODO: Level finish
        }
    }

    private bool GetNextSpline()
    {
        if (currentSplineIndex + 1 >= splines.Length) return false;

        currentSplineIndex++;
        return true;
    }

    public float GetCommonFrequency()
    {
        float commonFrequency = 0;
        foreach (var spline in splines)
        {
            commonFrequency += spline.frequency;
        }
        return commonFrequency;
    }

    public float GetCommonIndexes()
    {
        float commonIndexes = 0;
        foreach (var spline in splines)
        {
            commonIndexes += spline.bezierPoints.Count;
        }
        return commonIndexes;
    }

    public int GetCommonIndex(Vector3 position)
    {
        List<Vector3> points = new List<Vector3>();
        foreach (var spline in splines)
        {
            foreach (var point in spline.bezierPoints)
            {
                points.Add(point);
            }
        }
        
        return points.IndexOf(position); 
    }
}
