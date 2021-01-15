using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    public event Action onSplineEnded;

    [Header("SplineWalker Settings")]

    [SerializeField] private SplineWalkerMode mode;
    
    [SerializeField] private bool lookForward;

    [HideInInspector] public BezierSpline spline;
    
    [HideInInspector] public float speed;

    [HideInInspector] public int currentIndex;

    [HideInInspector] public List<Vector3> points;
    
    [HideInInspector] public float progress;

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

        Vector3 position = spline.GetPoint(progress);
        //transform.localPosition = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(progress));
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
            if(mode == SplineWalkerMode.Once)
            {
                onSplineEnded?.Invoke();
                points = spline.bezierPoints;
                return;
            }
            if(mode == SplineWalkerMode.Loop)
            {
                progress = 0f;
                currentIndex = 0;
            }
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


