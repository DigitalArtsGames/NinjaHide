using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] private CinemachineVirtualCamera gameCamera;
    [SerializeField] private CinemachineVirtualCamera mainMenuCamera;

    private bool isGameCamera;
    private Transform playerTransform;

    public void Start()
    {
        //SwitchToMainMenuCamera();
        //SwitchToGameCamera();
    }

    public void Update()
    {
        FindCamera();
    }

    public void LateUpdate()
    {
        if (playerTransform != null)
        {
            gameCamera.transform.position = playerTransform.position + new Vector3(13, 13, 0);

            gameCamera.transform.rotation = Quaternion.Euler(35, -90, 0);
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