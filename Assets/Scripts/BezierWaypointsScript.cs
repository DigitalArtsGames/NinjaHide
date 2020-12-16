using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class BezierWaypointsScript 
{
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 p01 = Vector3.Lerp(p0, p1, t);
        Vector3 p02 = Vector3.Lerp(p1, p2, t);
        Vector3 p03 = Vector3.Lerp(p2, p3, t);

        Vector3 p012 = Vector3.Lerp(p01, p02, t);
        Vector3 p123 = Vector3.Lerp(p02, p03, t);
        
        Vector3 p0123 = Vector3.Lerp(p012, p123, t);

        return p0123;
    }

    public static Vector3 QuadraticCurve(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 p0 = Vector3.Lerp(a, b, t);
        Vector3 p1 = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(p0, p1, t);
    }

    public static Vector3 CubicCurve(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 p0 = QuadraticCurve(a, b, c, t);
        Vector3 p1 = QuadraticCurve(b, c, d, t);
        
        return Vector3.Lerp(p0, p1, t);
    }

    

}
