using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animationController;

    private void Update()
    {
        ManageAnimation();
    }

    void ManageAnimation()
    {
        if(PlayerScript.Instance.isRunning)
        {
            animationController.SetBool("isRunning", true);
        }
        else
        {
            animationController.SetBool("isRunning", false);
        }

        if(PlayerScript.Instance.isHiding)
        {
            animationController.SetBool("isHiding", true);
        }
        else
        {
            animationController.SetBool("isHiding", false);
        }
    }
}
