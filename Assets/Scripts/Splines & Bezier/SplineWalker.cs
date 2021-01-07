using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{

    public bool isNPC;

    public SplineWalkerMode mode;

    private bool goingForward = true;

    public BezierSpline spline;

    public float runDuration;
    public float crouchDuration;

    private float duration;

    [HideInInspector] public float progress;

    public bool lookForward;

    private void Update()
    {
        Crouch();
        if (goingForward)
        {
            progress += Time.deltaTime / duration;
            if (progress > 1f)
            {
                if (mode == SplineWalkerMode.Once)
                {
                    progress = 1f;
                }
                else if (mode == SplineWalkerMode.Loop)
                {
                    progress -= 1f;
                }
                else
                {
                    progress = 2f - progress;
                    goingForward = false;
                }
            }
        }
        else
        {
            progress -= Time.deltaTime / duration;
            if (progress < 0f)
            {
                progress = -progress;
                goingForward = true;
            }
        }

        Vector3 position = spline.GetPoint(progress);
        transform.localPosition = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(progress));
        }
    }

    void Crouch()
    {
        if (Input.GetButton("Crouch") && !isNPC)
        {
            duration = crouchDuration;
        }
        else
        {
            duration = runDuration;
        }
    }
}
