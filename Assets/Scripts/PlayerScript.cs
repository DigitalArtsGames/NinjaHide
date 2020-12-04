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

    }

    void Update()
    {
        //GameObject go = GameObject.Find("MainCamera");
        //go.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        ToWaypointsMover();
        Crouch();
        sphereCollider = GetComponent<SphereCollider>();

        if (enemyTarget == null)
            return;

        Vector3 dir = enemyTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 3).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hostage")
        {
            Destroy(other.gameObject);
            score++;
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
