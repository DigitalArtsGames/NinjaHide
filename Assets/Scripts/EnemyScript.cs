using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public string targetTag;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Transform target;
    private SphereCollider sphereCollider;

    void Update()
    {
        sphereCollider = GetComponent<SphereCollider>();
        if (target == null)
            return;
        Invoke("Shoot()", 1f);
    }

    void Shoot()
    {
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletScript bullet = bulletGameObject.GetComponent<BulletScript>();
        if (bullet != null)
        {
            bullet.SetTarget(target);
        }
        //Debug.Log("BUX");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag)
        {
            target = other.transform;
            //Debug.Log("НАШЕЛ ЗАРАЗУ");
        }
    }
}
