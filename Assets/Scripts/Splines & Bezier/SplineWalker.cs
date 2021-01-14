using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    public event Action onSplineEnded;

    [HideInInspector] public int speed;

    public BezierSpline spline;

    public float progress;

    public SplineWalkerMode mode;

    public int currentIndex;

    [HideInInspector] public List<Vector3> points;

    private void Start()
    {
        points = spline.bezierPoints;
    }

    [SerializeField] private float nextPointTreshhold = 0.01f;
    private void Update()
    {
        progress = spline.GetProgress(currentIndex);
        if(points.Count > currentIndex)
        {
            if(Vector3.Distance(points[currentIndex], transform.position) < nextPointTreshhold)
            {
                GetNextPoint();
                //progress += Time.deltaTime;
            }

            var target = points[currentIndex];
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
        if (currentIndex + 1 >= points.Count)
        {
            onSplineEnded?.Invoke();
            points = spline.bezierPoints;
            return;
        }
        currentIndex++;
    }

    public void UpdateTarget()
    {

    }

    public void SetSpeed(int speed)
    {
        this.speed = speed;
    }

    private void GetProgress()
    {
        
    }

}


