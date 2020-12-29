using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereColliderScript : MonoBehaviour
{
    public List<GameObject> enemies;

    private void Update()
    {
        ClearRemovedEnemies();
    }

    private void ClearRemovedEnemies()
    {
        foreach (GameObject enemy in enemies.ToArray())
        {
            if(enemy == null)
            {
                enemies.Remove(enemy);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }
}
