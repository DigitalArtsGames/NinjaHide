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
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    private int score;

    Vector3 tempPosition;
    private Collider hidingSpot;
    private bool canHide;
    private bool isHiding;
    private SphereCollider sphereCollider;
    private float speed;
    private Transform waypoint;
    private int waypointIndex = 0;
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        enemyTarget = null;
        score = 0;
        waypoint = WaypointsScript.waypoints[0];
        sphereCollider = GetComponent<SphereCollider>();

    }

    void Update()
    {
        //GameObject go = GameObject.Find("MainCamera");
        //go.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        Hide();

        if (!isHiding)
        {
            ToWaypointsMover();
            Crouch();
        }
        //sphereCollider = GetComponent<SphereCollider>();

        if (enemyTarget == null)
            return;

        RotatePlayer();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
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
            hidingSpot = other;
        }
    }

    void Hide()
    {
        if (canHide == true)
        {
            if (Input.GetButtonDown("Hide"))
            {
                Debug.Log("I am hiding! He-he he");
                tempPosition = transform.position;
                transform.position = hidingSpot.transform.position;
                isHiding = true;
            }
            if (Input.GetButtonUp("Hide"))
            {
                transform.position = tempPosition;
                isHiding = false;
                tempPosition = new Vector3();
            }
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

    void ToWaypointsMover()
    {
        Vector3 dir = waypoint.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, waypoint.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= WaypointsScript.waypoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        waypoint = WaypointsScript.waypoints[waypointIndex];
    }

    void Crouch()
    {
        if (Input.GetButton("Crouch"))
        {
            speed = crouchSpeed;
        }
        else
        {
            speed = runSpeed;
        }
    }
}
