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
        currentSplineIndex = 0;
        splines = GetComponentsInChildren<BezierSpline>();
        playerSplineWalker.spline = splines[currentSplineIndex];
    }

    private void Update()
    {
        GetNextSpline();
    }

    public void GetNextSpline()
    {
        if (PlayerScript.splineWalker.progress == 1)
        {
            if (currentSplineIndex + 1 >= splines.Length) return;

            currentSplineIndex++;
            playerSplineWalker.progress = 0;
            playerSplineWalker.spline = splines[currentSplineIndex];
        }
    }
}
