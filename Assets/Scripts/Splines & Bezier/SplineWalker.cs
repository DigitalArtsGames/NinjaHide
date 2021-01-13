using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    public event Action onSplineEnded;

    public bool isNPC;

    public int speed = 20;

    public SplineWalkerMode mode;

    private bool goingForward = true;

    public BezierSpline spline;

    public float runDuration;
    public float crouchDuration;

    public int currentIndex;

    [SerializeField] private float nextPointTreshhold = 0.01f;
    private void Update()
    {
        if(spline.transform.childCount > currentIndex)
        {
            if(Vector3.Distance(spline.transform.GetChild(currentIndex).position, transform.position) < nextPointTreshhold)
            {
                GetNextPoint();
            }

            var target = spline.transform.GetChild(currentIndex).position;
            print(target);  
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        }
    }


    private void GetNextPoint()
    {
        if (currentIndex + 1 >= spline.transform.childCount)
        {
            onSplineEnded?.Invoke();
            return;
        }

        currentIndex++;
    }

}


