using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animationController;
    [SerializeField] private SplineWalker splineWalker;


    private Vector3 playerDirection;
    private void Update()
    {
        ManageAnimation();
    }

    void ManageAnimation()
    {
        if (PlayerScript.Instance.GetTarget() != null)
        {
            playerDirection = PlayerScript.Instance.GetPlayerDirection();
        }
        else
        {
            playerDirection = splineWalker.GetPlayerDirection();
        }

        //playerDirection = PlayerScript.Instance.GetPlayerDirection();
        float velocityX = playerDirection.x * Time.deltaTime;
        float velocityZ = playerDirection.z * Time.deltaTime;

        animationController.SetFloat("MoveX", velocityX);
        animationController.SetFloat("MoveZ", velocityZ);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, playerDirection);
    }
}
