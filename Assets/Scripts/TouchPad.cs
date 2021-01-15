using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerScript.Instance.buttonPressed = true;    
        PlayerScript.Instance.isHiding = PlayerScript.Instance.canHide;    
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerScript.Instance.buttonPressed = false;
    }
}
