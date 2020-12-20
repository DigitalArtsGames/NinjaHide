using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    //Не критичные предупреждения, потом нужно пофиксить
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public Transform camera;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    void Update()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}
