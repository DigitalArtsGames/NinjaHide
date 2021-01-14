using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    public event Action onSplineEnded;

    public int speed = 20;

    public BezierSpline spline;

    public float progress;

    public SplineWalkerMode mode;

    public int currentIndex;

    [SerializeField] private float nextPointTreshhold = 0.01f;
    private void Update()
    {
        if(spline.transform.childCount > currentIndex)
        {
            if(Vector3.Distance(spline.transform.GetChild(currentIndex).position, transform.position) < nextPointTreshhold)
            {
                GetNextPoint();
                progress += Time.deltaTime;
            }

            var target = spline.transform.GetChild(currentIndex).position;
            //print(target);  
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    public void SetPositionIndex(int index)
    {
        currentIndex = index;
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
    private void GetProgress()
    {

    }

}


