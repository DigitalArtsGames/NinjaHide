using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera gameCamera;
    [SerializeField] private CinemachineVirtualCamera mainMenuCamera;

    private bool isGameCamera;
    private Transform playerTransform;

    public void Start()
    {
        SwitchToMainMenuCamera();
    }

    public void Update()
    {
        FindCamera();
    }

    public void LateUpdate()
    {
        if(gameCamera.Follow == null)
        {
            if(playerTransform!= null)
            {
                gameCamera.Follow = playerTransform;
            }
        }
    }

    private void FindCamera()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void SwitchToGameCamera()
    {
        mainMenuCamera.gameObject.GetComponent<CinemachineVirtualCamera>().enabled = false;
        gameCamera.gameObject.GetComponent<CinemachineVirtualCamera>().enabled = true;
        isGameCamera = true;
        
    }
    
    public void SwitchToMainMenuCamera()
    {
        gameCamera.gameObject.GetComponent<CinemachineVirtualCamera>().enabled = false;
        mainMenuCamera.gameObject.GetComponent<CinemachineVirtualCamera>().enabled = true;
        gameCamera.Follow = null;
    }
}
