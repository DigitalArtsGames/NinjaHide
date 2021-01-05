﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Transform enemyTarget;

    [Header("Prefabs and Points")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Player Parameters")]
    public float runSpeed = 10f;
    public float crouchSpeed = 5f;
    public Material lineMaterial;
    public GameObject circlePrefab;

    [Header("Fire Rate")]
    private float fireRate = 1f;
    private float nextFire = 0f;

    private int score;

    private bool buttonPressed;
    private SplineWalker splineWalker;

    private GameObject hidingSpotNearby;
    private GameObject exitSpotNearby;

    private bool canHide;
    private SphereCollider sphereCollider;

    private LineRenderer lineRenderer;

    private ObjectPoolerScript objectPooler;

    void Start()
    {
        objectPooler = ObjectPoolerScript.Instance;
        enemyTarget = null;
        score = 0;
        sphereCollider = GetComponentInChildren<SphereCollider>();
        splineWalker = GetComponent<SplineWalker>();
        StartCoroutine(UpdateTarget());

    }
    void Update()
    {
        RotateToPlayer();
        hidingSpotNearby = GameObject.FindGameObjectWithTag("HidingSpot");
        exitSpotNearby = GameObject.FindGameObjectWithTag("ExitSpot");
        FindHidingSpot();

        if (enemyTarget == null)
            return;


        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    //Поварачивает игрока в сторону стрельбы
    void RotateToPlayer()
    {
        if (enemyTarget != null)
        {
            Vector3 targetDirection = enemyTarget.position - transform.position;
            float singleStep = 1 * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

        }
    }

    void RotatePlayer()
    {
        Vector3 dir = enemyTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);


        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 3).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        Debug.DrawRay(transform.position, dir, Color.red);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hostage"))
        {
            Destroy(other.gameObject);
            score++;
        }
        if (other.gameObject.CompareTag("HidingSpot"))
        {
            canHide = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("HidingSpot"))
        {
            canHide = false;
        }
    }

    void Shoot()
    {
        //Pool usage
        GameObject bulletGameObject = objectPooler.SpawnFromPool("Bullet", firePoint.position, firePoint.rotation);
        BulletScript bullet = bulletGameObject.GetComponent<BulletScript>();
        if (bullet != null)
        {
            bullet.SetTarget(enemyTarget);
        }
    }

    //Обновляет врагов
    public IEnumerator UpdateTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            List<GameObject> enemies = sphereCollider.GetComponent<SphereColliderScript>().enemies;
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= sphereCollider.radius)
            {
                enemyTarget = nearestEnemy.transform;
            }
            else
            {
                enemyTarget = null;
            }
        }
    }

    void IsPressedButton()
    {
        //if (Input.GetButtonDown("Hide") && canHide)
        if (Input.GetButtonDown("Fire1") && canHide)
            buttonPressed = !buttonPressed;
    }

    void ShowPathToHidingSpot(Vector3 start, Vector3 end, Color color)
    {
        Debug.DrawLine(start, end, color);
    }

    void FindHidingSpot()
    {
        IsPressedButton();
        int speed = 2;

        if (hidingSpotNearby != null && canHide)
        {
            ShowPathToHidingSpot(transform.position, hidingSpotNearby.transform.position, Color.red);
            if (gameObject.GetComponent<LineRenderer>() == null)
            {
                lineRenderer = gameObject.AddComponent<LineRenderer>();
            }
            lineRenderer.material = lineMaterial;
            lineRenderer.widthMultiplier = 0.2f;

            Vector3[] points = { transform.position, hidingSpotNearby.transform.position };

            if (!GameObject.FindGameObjectWithTag("Circle"))
            {
                Instantiate(circlePrefab, hidingSpotNearby.transform.position, hidingSpotNearby.transform.rotation);
            }

            for (int i = 0; i < points.Length; i++)
            {
                Vector3 newpos = new Vector3(points[i].x, points[i].y - 0.9f, points[i].z);
                points[i].Set(newpos.x, newpos.y, newpos.z);
            }

            for (int i = 0; i < points.Length; i++)
            {
                lineRenderer.SetPositions(points);
            }
        }
        else
        {
            Destroy(lineRenderer);
            Destroy(GameObject.FindGameObjectWithTag("Circle"));
        }

        if (buttonPressed)
        {
            splineWalker.enabled = false;
            if (transform.position != hidingSpotNearby.transform.position)
            {
                Vector3 dirToHidingSpot = (hidingSpotNearby.transform.position - transform.position);
                transform.Translate(dirToHidingSpot * Time.deltaTime * speed);
            }
        }
        else
        {
            if (transform.position != exitSpotNearby.transform.position)
            {
                Vector3 dirToExitSpot = (exitSpotNearby.transform.position - transform.position);
                transform.Translate(dirToExitSpot * Time.deltaTime * speed);
            }
            splineWalker.enabled = true;
        }
    }
}
