using System.Collections;
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

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
        enemyTarget = null;
        score = 0;
        sphereCollider = GetComponentInChildren<SphereCollider>();
        splineWalker = GetComponent<SplineWalker>();

    }
    void Update()
    {
        hidingSpotNearby = GameObject.FindGameObjectWithTag("HidingSpot");
        exitSpotNearby = GameObject.FindGameObjectWithTag("ExitSpot");
        FindHidingSpot();

        if (enemyTarget == null)
            return;

        RotatePlayer();

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    //Поварачивает игрока в сторону стрельбы
    void RotatePlayer()
    {
        Vector3 dir = enemyTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 3).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
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
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletScript bullet = bulletGameObject.GetComponent<BulletScript>();
        if (bullet != null)
        {
            bullet.SetTarget(enemyTarget);
        }
    }

    //Обновляет врагов
    public void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
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


    void IsPressedButton()
    {
        if (Input.GetButtonDown("Hide") && canHide)
            buttonPressed = !buttonPressed;
    }


    void FindHidingSpot()
    {
        IsPressedButton();
        int speed = 10;

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

    //void GetNextWaypoint()
    //{
    //    if (waypointIndex >= WaypointsScript.waypoints.Length - 1)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //    waypointIndex++;
    //    waypoint = WaypointsScript.waypoints[waypointIndex];
    //}

    //void ToWaypointsMover()
    //{
    //    Vector3 dir = waypoint.position - transform.position;
    //    transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

    //    if (Vector3.Distance(transform.position, waypoint.position) <= 0.2f)
    //    {
    //        GetNextWaypoint();
    //    }
    //}
}
