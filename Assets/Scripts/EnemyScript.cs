using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Main Settings")]
    public string targetTag;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public MeshScript meshScript;

    [Header("FOV and View Destance Settings")]
    public float viewDistance;
    public float fov;

    private MeshScript gm;
    private Transform target;
    private bool isAgressive;
    private SphereCollider sphereCollider;

    private void Start()
    {
        gm = Instantiate(meshScript, firePoint.position, Quaternion.Euler(90,0,90));
    }

    void Update()
    {
        gm.SetFov(fov);
        gm.SetViewDistance(viewDistance);

        sphereCollider = GetComponent<SphereCollider>();
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
            //Debug.Log("BUX");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag)
        {
            target = other.transform;
            isAgressive = true;
            //Debug.Log("НАШЕЛ ЗАРАЗУ");
        }
    }

    //Враг моментально переходит в неагрессивное состояние если игрок вышел из поле зрения
    void OnTriggerExit(Collider other)
    {
        if (other.tag == targetTag)
        {
            target = other.transform;
            isAgressive = false;
        }
    }
}
