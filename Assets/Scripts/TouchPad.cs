using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        if(PlayerScript.Instance.canHide)
        {
            PlayerScript.Instance.buttonPressed = true;    
            PlayerScript.Instance.isHiding = PlayerScript.Instance.canHide;
            PlayerScript.Instance.isRunning = false;
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(PlayerScript.Instance.canHide)
        {
            PlayerScript.Instance.buttonPressed = false;
        }
    }
}
