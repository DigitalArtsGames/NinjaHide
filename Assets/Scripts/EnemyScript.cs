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

    [Header("Fire Rate")]
    private float fireRate = 1f;
    private float nextFire = 0f;

    public SeeingBarScript seeingBarScript;
    public GameObject seeingBarObject;

    private int shotsCount;
    private Transform target;
    private bool isSeeingPlayer;
    private List<Transform> targets;

    void Start()
    {
        seeingBarScript.SetMaxSeeingValue(detectionTime);
        seeingBarScript.SetSeeingValue(0);
        seeingBarObject.SetActive(false);

        shotsCount = 0;
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
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                ShootWithChance();
            }
        }
        else
        {
            isSeeingPlayer = false;
            seeingBarObject.SetActive(false);
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

    void Shoot(Transform target)
    {
        GameObject bulletGameObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletScript bullet = bulletGameObject.GetComponent<BulletScript>();
        if (bullet != null)
        {
            bullet.SetTarget(target);
            shotsCount++;
        }
    }

    //Проверяет проходит ли шанс выстрела рандомному шансу
    void ShootWithChance()
    {
        int shotChance = GetRandomizeChance();
        int ranChance = Random.Range(0, 100);

        if (ranChance < shotChance)
        {
            Debug.Log("СТРЕЛЯЮ И ПОПАДАЮ >:)");
            Shoot(target);
        }
        else
        {
            Debug.Log("СТРЕЛЯЮ НО НЕ ПОПАДАЮ :(");

            GameObject fakeTarget = new GameObject();

            fakeTarget.transform.position = target.position;
            Vector3 newPos = new Vector3(target.position.x + 1, target.position.y + 1, target.position.z);

            fakeTarget.transform.SetPositionAndRotation(newPos, fakeTarget.transform.rotation);
            Shoot(fakeTarget.transform);
        }
    }

    //Вычисляет шанс выстрела
    int GetRandomizeChance()
    {
        //первый выстрел = 0
        if (shotsCount == 0)
        {
            return 0;
        }
        else if (shotsCount == 1)
        {
            return 50;
        }
        else if (shotsCount == 2)
        {
            return 90;
        }
        else
        {
            return 99;
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
            seeingBarObject.SetActive(true);
            currentDetectionValue++;
            seeingBarScript.SetSeeingValue(currentDetectionValue);
        }
        else
        {
            if (currentDetectionValue == 0)
            {
                return;
            }
            currentDetectionValue--;
            seeingBarScript.SetSeeingValue(currentDetectionValue);
        }
    }

}
