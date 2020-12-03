using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Transform enemyTarget;
    
    public float runSpeed = 10f;
    public float crouchSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private int score;

    private SphereCollider sphereCollider;
    private float speed;
    private Transform waypoint;
    private int waypointIndex = 0;
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        
        score = 0;
        waypoint = WaypointsScript.waypoints[0];

    }

    void Update()
    {
        ToWaypointsMover();
        Crouch();
        sphereCollider = GetComponent<SphereCollider>();
        
        if (enemyTarget == null)
            return;
        Shoot();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hostage")
        {
            Destroy(other.gameObject);
            score++;
            Debug.Log(score);
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
            Debug.Log("WIN!!");
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        waypoint = WaypointsScript.waypoints[waypointIndex];
    }

    void Crouch()
    {
        if(Input.GetButton("Crouch"))
        {
            speed = crouchSpeed;
        }
        else
        {
            speed = runSpeed;
        }
    }
}
