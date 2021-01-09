using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cinemaVirCam;

    private GameObject player;
    private Vector3 menuCameraPosition;
    private Quaternion menuCameraRotation;

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
