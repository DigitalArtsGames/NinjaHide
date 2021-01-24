using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;
    private Transform enemyTarget;

    [Header("Prefabs")]
    public GameObject bulletPrefab;
    public GameObject circlePrefab;
    public Transform firePoint;

    [Header("Player Parameters")]
    public int runSpeed = 10;
    public int crouchSpeed = 5;
    public Material lineMaterial;
    public float seeingRadius;

    [Header("Fire Rate")]
    private float fireRate = 1f;
    private float nextFire = 0f;

    private int score;

    [HideInInspector] public bool isRunning;
    [HideInInspector] public bool isShooting;
    [HideInInspector] public bool canHide;
    [HideInInspector] public bool isHiding;
    [HideInInspector] public bool isGoingRight;
    [HideInInspector] public bool buttonPressed;
    [HideInInspector] public static SplineWalker splineWalker;
    [HideInInspector] public Vector3 tempDir;

    private GameObject hidingSpotNearby;

    private LineRenderer lineRenderer;

    private ObjectPoolerScript objectPooler;

    void Start()
    {
        CheckInstance();
        objectPooler = ObjectPoolerScript.Instance;
        enemyTarget = null;
        score = 0;
        splineWalker = GetComponent<SplineWalker>();
        StartCoroutine(UpdateTarget());
    }

    void Update()
    {
        //RotatePlayer();

        //LookAtTarget(GetTarget(seeingRadius + 1f));
        CheckSpeed();
        GoToHidingSpot();

        if (enemyTarget == null)
            return;
        //print(enemyTarget);

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
        
    }

    public Vector3 GetHidingSpotDirection()
    {
        if(isGoingRight)
        {
            return hidingSpotNearby.transform.position - transform.position;
        } 
        else
        {
            //tempDir = hidingSpotNearby.transform.position;
            return transform.position - hidingSpotNearby.transform.position;
        }
    }

    public Vector3 GetPlayerDirection(float seeingRadius)
    {
        if(GetTarget(seeingRadius) != null)
        {
            Vector3 dir = GetTarget(seeingRadius).position - transform.position;
            return dir;
        }
        return Vector3.zero;
    }

    //public void LookAtTarget(Transform target)
    //{
    //    if (target != null)
    //    {
    //        splineWalker.enableLookForward = false;
    //        Vector3 dir = target.position - transform.position;
    //        Quaternion rotation = Quaternion.LookRotation(dir);
    //        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 10 * Time.deltaTime);
    //    }
    //    else
    //    {
    //        splineWalker.enableLookForward = true;
    //    }
    //}

    public void CheckInstance()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void CheckSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            splineWalker.speed = crouchSpeed;
            isRunning = false;
        }
        else
        {
            if(!isHiding)
                isRunning = true;
            
            splineWalker.speed = runSpeed;
        }
    }

    void RotatePlayer()
    {
        if (enemyTarget != null)
        {
            Vector3 targetDirection = enemyTarget.position - transform.position;
            float singleStep = 1 * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            //Debug.DrawLine(transform.position, newDirection, Color.red);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
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
            hidingSpotNearby = other.gameObject;

            HidingSpotScript hidingSpot = hidingSpotNearby.GetComponent<HidingSpotScript>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("HidingSpot"))
        {
            canHide = false;
            hidingSpotNearby = null;
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
            enemyTarget = GetTarget(seeingRadius);
        }
    }

    public Transform GetTarget(float seeingRadius)
    {
        //List<GameObject> enemies = sphereCollider.GetComponent<SphereColliderScript>().enemies;
        List<GameObject> enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy != null && shortestDistance <= seeingRadius)
        {
            Debug.DrawLine(transform.position, nearestEnemy.transform.position, Color.green);
            return nearestEnemy.transform;
        }
        else
        {
            return null;
        }
    }

    void IsPressedButton()
    {
        if (Input.GetMouseButtonDown(0) && canHide)
        {
            buttonPressed = true;
            isHiding = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            buttonPressed = false;
        }
    }

    void ShowPathToHidingSpot(Vector3 start, Vector3 end, Color color)
    {
        Debug.DrawLine(start, end, color);
    }

    List<Vector3> GetSplinePoints()
    {
        return splineWalker.points;
    }

    public Vector3 FindExitSpotNearby()
    {
        Vector3 bestTarget = Vector3.zero;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Vector3 potentialTarget in GetSplinePoints())
        {
            Vector3 directionToTarget = potentialTarget - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;

            }
        }
        return bestTarget;
    }

    void GoToHidingSpot()
    {
        IsPressedButton();

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

        if (canHide)
        {
            if (buttonPressed)
            {
                isGoingRight = true;
                splineWalker.enabled = false;
                transform.position = Vector3.MoveTowards(transform.position, hidingSpotNearby.transform.position, 10 * Time.deltaTime);
            }
            else
            {
                if (isHiding)
                {
                    if (Vector3.Distance(transform.position, FindExitSpotNearby()) > 0f)
                    {
                        isGoingRight = false;
                        transform.position = Vector3.MoveTowards(transform.position, FindExitSpotNearby(), Time.deltaTime * 10);
                    }
                    else
                    {
                        isHiding = false;
                        //splineWalker.SetPositionIndex(GetSplinePoints().IndexOf(FindExitSpotNearby()));
                        splineWalker.SetPositionIndex(FindExitSpotNearby());
                        splineWalker.enabled = true;
                    }
                }
            }
        }
    }
}
