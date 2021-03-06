﻿using System.Collections.Generic;
using UnityEngine;


public class SplineDecorator : MonoBehaviour
{

	public BezierSpline spline;

	public List<Vector3> points;

	public int frequency;

	public bool lookForward;

	public Transform[] items;

	private void Start()
	{
		//points = new List<Vector3>();
		CreatePoints();
	}

	public void CreatePoints()
	{
		if (frequency <= 0 || items == null || items.Length == 0)
		{
			return;
		}
		float stepSize = frequency * items.Length;
		if (spline.Loop || stepSize == 1)
		{
			stepSize = 1f / stepSize;
		}
		else
		{
			stepSize = 1f / (stepSize - 1);
		}

		for (int p = 0, f = 0; f < frequency; f++)
		{
			for (int i = 0; i < items.Length; i++, p++)
			{
				//Transform item = Instantiate(items[i]) as Transform;
				Vector3 position = spline.GetPoint(p * stepSize);
				//item.transform.localPosition = position;
				points.Add(position);
				if (lookForward)
				{
					//item.transform.LookAt(position + spline.GetDirection(p * stepSize));
				}
				//item.transform.parent = transform;
			}
		}
	}
}
