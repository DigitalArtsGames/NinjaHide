using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    public Transform camera;

    void Update()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}
