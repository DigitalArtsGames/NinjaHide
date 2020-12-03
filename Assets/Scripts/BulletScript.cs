using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    public void SetTarget(Transform setTraget)
    {
        target = setTraget;
    }

    void Update()
    {
        ChaseTarget();
    }

    public void HitTarget()
    {
        Debug.Log("HITT!");
        Destroy(gameObject);
        Destroy(target.gameObject);
    }

    public void ChaseTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
}
