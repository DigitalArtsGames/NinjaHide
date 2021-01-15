using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTargetScript : MonoBehaviour
{
    Transform target;

    public void LookAtTarget(Transform target)
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5 * Time.deltaTime);
        }
    }
}
