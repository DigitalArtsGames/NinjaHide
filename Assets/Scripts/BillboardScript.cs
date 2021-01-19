using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    //Не критичные предупреждения, потом нужно пофиксить

    public new Transform camera;

    void FixedUpdate()
    {
        if(camera != null)
        {
            transform.LookAt(transform.position + camera.forward);
        } else
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
    }
}
