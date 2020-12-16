using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BezierWayNavigator : MonoBehaviour
{
    public Transform p0;
    public Transform p1;
    public Transform p2;
    public Transform p3;

    public Vector3[] points;

    [Range(0,1)]
    public float t;

    private void Update()
    {
        transform.position = BezierWaypointsScript.CubicCurve(p0.position, p1.position, p2.position, p3.position, t);
    }

    private void OnDrawGizmos()
    {
        int sigmentsNumber = 20;
        Vector3 preveousPoint = p0.position;

        for (int i = 0; i < sigmentsNumber + 1; i++)
        {
            float parametr = (float)i / sigmentsNumber;
            Vector3 point = BezierWaypointsScript.GetPoint(p0.position, p1.position, p2.position, p3.position, parametr);
            Gizmos.DrawLine(preveousPoint, point);
            preveousPoint = point;
        }

    }
}
