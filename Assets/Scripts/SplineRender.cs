using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineRender : MonoBehaviour
{
    public SplineDecorator bezierSpline;
    public Material lineMaterial;
    public bool isGlowing;

    private Vector3[] points;
    private LineRenderer lineRenderer;
    private bool isEnable;

    private void Start()
    {
        if(isGlowing)
        {
            InvokeRepeating("EnableCircle", 0f, 0.3f);
            //StartCoroutine(Glow());
        }

        if (gameObject.GetComponent<LineRenderer>() == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        points = bezierSpline.points.ToArray();

        for (int i = 0; i < points.Length; i++)
        {
            Vector3 newpos = new Vector3(points[i].x, points[i].y - 0.9f, points[i].z);
            points[i].Set(newpos.x, newpos.y, newpos.z);
        }
        
        lineRenderer.material = lineMaterial;
        lineRenderer.widthMultiplier = 0.2f;
        
        lineRenderer.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }  
        
    }

    private void Update()
    {
    }

    IEnumerator Glow()
    {
        yield return new WaitForSeconds(0.5f);
        EnableCircle();
    }

    void EnableCircle()
    {
        if(isEnable)
        {
            lineRenderer.enabled = false;
            isEnable = false;
        } else
        {
            lineRenderer.enabled = true;
            isEnable = true;
        }
    }

}
