using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cinemaVirCam;

    void Update()
    {
        if(cinemaVirCam.Follow == null)
            cinemaVirCam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
