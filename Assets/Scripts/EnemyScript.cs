using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Main Settings")]
    public string targetTag;
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("FOV and View Destance Settings")]
    public float viewDistance;
    public float fov;
    public GameObject eyes;

    private Transform target;
    private bool isAgressive;

    private void Start()
    {
        
    }

    void Update()
    {
        RotatePlayer();

        if (target == null)
            return;
        Invoke("Shoot", 2f);
    }

    //Если противнику нужно время для перехода в не агрессивное состояние
    //IEnumerator ColdDown()
    //{
    //    yield return new WaitForSeconds(3f);
    //    isAgressive = false;
    //}

    public void Shoot()
    {
        if(isAgressive)
        {
            GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletScript bullet = bulletGameObject.GetComponent<BulletScript>();
            if (bullet != null)
            {
                bullet.SetTarget(target);
            }
        }
    }

    void RotatePlayer()
    {
        if(isAgressive && target != null)
        {
            Vector3 targetDirection = target.position - transform.position;
            float singleStep = 1 * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            Debug.DrawRay(transform.position, newDirection, Color.red);
            transform.rotation = Quaternion.LookRotation(newDirection);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag)
        {
            target = other.transform;
            isAgressive = true;
            Debug.Log("НАШЕЛ ЗАРАЗУ");
        }
    }

    //Враг моментально переходит в неагрессивное состояние если игрок вышел из поле зрения
    void OnTriggerExit(Collider other)
    {
        if (other.tag == targetTag)
        {
            //target = other.transform;
            isAgressive = false;
        }
    }
}
