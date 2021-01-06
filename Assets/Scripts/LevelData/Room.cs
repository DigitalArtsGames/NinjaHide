using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Player Settings")]
    public GameObject player;
    public GameObject playerSpline;

    [Header("Enemies Settings")]
    public EnemyScript[] enemiesScript;
    public GameObject[] enemiesSplines;
    
    public GameObject[] roomObjects;

    [HideInInspector] public Vector3 playerPosition;

    private void Start()
    {
        playerPosition = player.transform.position;
    }
}
