using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDrawingScript : MonoBehaviour
{
    public int segments;
    public float xradius;
    public float yradius;
    LineRenderer lineRenderer;
    private bool isEnable;

    void Start()
    {
        isEnable = true;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(segments + 1);
        lineRenderer.useWorldSpace = false;
        CreatePoints();
    }
    void Update()
    {
        InvokeRepeating("EnableCircle", 0f, 0.5f);
    }


    void CreatePoints()
    {
        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            lineRenderer.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }



    void EnableCircle()
    {
        if (isEnable)
        {
            lineRenderer.enabled = false;
            isEnable = false;
        }
        else
        {
            lineRenderer.enabled = true;
            isEnable = true;
        }
    }

}
