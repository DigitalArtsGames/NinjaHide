using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{

	public BezierSpline spline;

	public float runDuration;
	public float crouchDuration;

	private float duration;

	private float progress;

	private void Update()
	{
		Crouch();
		progress += Time.deltaTime / duration;
		if (progress > 1f)
		{
			progress = 1f;
		}
		transform.localPosition = spline.GetPoint(progress);
	}

	void Crouch()
	{
		if (Input.GetButton("Crouch"))
		{
			duration = crouchDuration;
		}
		else
		{
			duration = runDuration;
		}
	}
}
