using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    public CinemachineVirtualCamera cinemaVirCam;

    private GameObject player;
    public Vector3 menuCameraPosition;
    public Quaternion menuCameraRotation;

    private void Start()
    {
        menuCameraPosition = cinemaVirCam.transform.position;
        menuCameraRotation = cinemaVirCam.transform.rotation;
    }

    void Update()
    {
        SetFollow();
    }

    void SetFollow()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (cinemaVirCam.Follow == null)
        {
            if (player != null)
            {
                cinemaVirCam.Follow = player.transform;
                cinemaVirCam.transform.rotation = Quaternion.Euler(35, -90, 0);
            }
            else
            {
                cinemaVirCam.transform.position = menuCameraPosition;
                cinemaVirCam.transform.rotation = menuCameraRotation;
            }
        }

    }
}
