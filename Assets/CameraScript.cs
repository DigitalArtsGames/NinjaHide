using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cinemaVirCam;

    private GameObject player;

    void Update()
    {
        SetFollow();
    }

    void SetFollow()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(cinemaVirCam.Follow == null)
        {
            if (player != null)
            {
                cinemaVirCam.Follow = player.transform;
                cinemaVirCam.transform.rotation = Quaternion.Euler(35, -90, 0);
            }
            //} else
            //{
            //    Debug.LogWarning("There is no Player to follow!");
            //}
        }
    }
}
