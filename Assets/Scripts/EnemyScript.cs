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

    public int maxPossibility;
    public int minPossibility;

    private int shotsCount;
    private Transform target;
    private bool isSeeingPlayer;
    private List<Transform> targets;
    private ObjectPoolerScript objectPooler;

    void Start()
    {
        objectPooler = ObjectPoolerScript.Instance;

        seeingBarScript.SetMaxSeeingValue(detectionTime);
        seeingBarScript.SetSeeingValue(0);
        seeingBarObject.SetActive(false);

        shotsCount = 0;
    }

    void FixedUpdate()
    {
        targets = GetComponent<FieldOfView>().visibleTargets;
        GetTargets();
        SeeingPlayer();
        if (target != null)
        {
            isSeeingPlayer = true;
            RotateToPlayer();
            
            if(currentDetectionValue == seeingBarScript.GetMaxValue())
            {
                //Скорость стрельбы
                if (Time.time > nextFire)
                {
                    ShootWithChance();
                    nextFire = Time.time + fireRate;
                }
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
        GameObject bulletGameObject = objectPooler.SpawnFromPool("Bullet", firePoint.position, firePoint.rotation);
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
        GetRandomizeChance();
        int ranChance = Random.Range(0, 100);

        if (minPossibility < ranChance && ranChance < maxPossibility)
        {
            Shoot(target);
        }
        else
        {
            GameObject fakeTarget = new GameObject();

            fakeTarget.transform.position = target.position;
            Vector3 newPos = new Vector3(target.position.x + 1, target.position.y + 1, target.position.z);

            fakeTarget.transform.SetPositionAndRotation(newPos, fakeTarget.transform.rotation);
            Shoot(fakeTarget.transform);
        }
    }

    //Вычисляет шанс выстрела
    void GetRandomizeChance()
    {
        //шанс попадания
        if (shotsCount == 0)
        {
            //0%
            minPossibility = 0;
            maxPossibility = 0;
        }
        else if (shotsCount == 1)
        {
            //50%
            minPossibility = 50;
            maxPossibility = 100;
        }
        else if (shotsCount == 2)
        {
            //90%
            minPossibility = 10;
            maxPossibility = 100;
        }
        else
        {
            //100%
            minPossibility = 0;
            maxPossibility = 100;
        }
    }

    void RotateToPlayer()
    {
        Vector3 targetDirection = target.position - transform.position;
        float singleStep = 1 * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        //Debug.DrawLine(transform.position, newDirection, Color.red);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void SeeingPlayer()
    {
        if (isSeeingPlayer)
        {
            seeingBarObject.SetActive(true);
            if(currentDetectionValue != seeingBarScript.GetMaxValue())
            {
                currentDetectionValue++;
            }
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
