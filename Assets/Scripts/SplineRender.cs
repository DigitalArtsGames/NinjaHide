using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineRender : MonoBehaviour
{
    //public SplineDecorator splineDecorator;
    public Material lineMaterial;
    public bool isGlowing;

    private Vector3[] points;
    private LineRenderer lineRenderer;
    private bool isEnable;

    private BezierSpline bezierSpline;

    public void RenderLine()
    {
        bezierSpline = GetComponent<BezierSpline>();

        if (gameObject.GetComponent<LineRenderer>() == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        
        points = bezierSpline.bezierPoints.ToArray();

        for (int i = 0; i < points.Length; i++)
        {
            Vector3 newpos = new Vector3(points[i].x, points[i].y - 0.9f, points[i].z);
            points[i].Set(newpos.x, newpos.y, newpos.z);

        }
        
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);

        lineRenderer.material = lineMaterial;

 
        lineRenderer.widthMultiplier = 0.2f;  
        
        if(isGlowing)
        {
            //Pokachto
            //InvokeRepeating("EnableCircle", 0f, 0.3f);
            StartCoroutine(Glow());
        }
    }

    private void Update()
    {
    }

    IEnumerator Glow()
    {
        while(true)
        {
            EnableCircle();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void EnableCircle()
    {
        isEnable = (lineRenderer.enabled = !isEnable);
        //if (isEnable)
        //{
        //    lineRenderer.enabled = false;
        //    isEnable = false;
        //} else
        //{
        //    lineRenderer.enabled = true;
        //    isEnable = true;
        //}
    }

}
