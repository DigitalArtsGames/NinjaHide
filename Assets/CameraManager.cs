using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject gameCamera;
    public GameObject mainMenuCamera;

    public void Start()
    {
        SwitchToMainMenuCamera();
    }

    public void SwitchToGameCamera()
    {
        mainMenuCamera.SetActive(false);
        gameCamera.SetActive(true);
        //Follow
    }
    
    public void SwitchToMainMenuCamera()
    {
        gameCamera.SetActive(false);
        mainMenuCamera.SetActive(true);
    }
}
