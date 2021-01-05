using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject player;
    public GameObject playerSpline;
    
    public EnemyScript[] enemiesScript;
    public GameObject enemySpline;
    
    public GameObject[] roomObjects;

    [HideInInspector] public Vector3 playerPosition;

    private void Start()
    {
        playerPosition = player.transform.position;
    }
}
