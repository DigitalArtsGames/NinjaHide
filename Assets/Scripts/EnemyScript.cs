using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Main Settings")]
    public string targetTag;
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Player Detection Settings")]
    public int detectionTime;
    public int currentDetectionValue;

    public SeeingBarScript seeingBarScript;

    private Transform target;
    private bool isSeeingPlayer;
    private List<Transform> targets;

    void Start()
    {
        seeingBarScript.SetMaxSeeingValue(detectionTime);
        seeingBarScript.SetSeeingValue(0);
    }

    void Update()
    {
        targets = GetComponent<FieldOfView>().visibleTargets;
        GetTargets();
        SeeingPlayer();
        if (target != null)
        {
            isSeeingPlayer = true;
            RotateToPlayer();
            //Shoot();
        }
        else
        {
            isSeeingPlayer = false;
        }
    }

    void GetTargets()
    {
        target = null;
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

    void RotateToPlayer()
    {
        Vector3 targetDirection = target.position - transform.position;
        float singleStep = 1 * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        Debug.DrawRay(transform.position, newDirection, Color.red);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void SeeingPlayer()
    {
        if (isSeeingPlayer)
        {
            currentDetectionValue++;
            seeingBarScript.SetSeeingValue(currentDetectionValue);
        }
        else
        {
            if(currentDetectionValue == 0)
            {
                return;
            }
            currentDetectionValue--;
            seeingBarScript.SetSeeingValue(currentDetectionValue);
        }
    }

}
