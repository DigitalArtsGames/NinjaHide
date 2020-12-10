using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Main Settings")]
    public string targetTag;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Transform target;

    void Update()
    {
        GetTargets();
        if(target != null)
        {
            RotatePlayer();
            Shoot();
        }
    }

    void GetTargets()
    {
        List<Transform> targets = GetComponent<FieldOfView>().visibleTargets;
        foreach (var target in targets)
        {
            this.target = target;
        }
    }

    void Shoot()
    {
        GameObject bulletGameObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletScript bullet = bulletGameObject.GetComponent<BulletScript>();
        if (bullet != null)
        {
            bullet.SetTarget(target);
        }
    }

    void RotatePlayer()
    {
        Vector3 targetDirection = target.position - transform.position;
        float singleStep = 1 * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        Debug.DrawRay(transform.position, newDirection, Color.red);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
