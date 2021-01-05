using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObject : MonoBehaviour
{
    public GameObject prefab;
    [HideInInspector] public Vector3 position;

    private void Start()
    {
        position = transform.position;
    }
}
