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
}
