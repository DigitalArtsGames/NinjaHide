using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animationController;
    [SerializeField] private SplineWalker splineWalker;
    [SerializeField] private PlayerScript playerScript;


    private Vector3 playerDirection;
    private void Update()
    {
        ManageAnimation();
    }

    void ManageAnimation()
    {

        //playerDirection = PlayerScript.splineWalker.GetPlayerDirection();
        if (playerScript.GetTarget() != null)
        {
            playerDirection = playerScript.GetPlayerDirection();
            Debug.Log("Direction to Enemy!");
        }
        else if(playerScript.isHiding)
        {
            playerDirection = playerScript.GetHidingSpotDirection();
        }
        else
        {
            playerDirection = transform.rotation * Vector3.right;
            playerDirection = transform.InverseTransformDirection(playerDirection);
        }

        float velocityX = +playerDirection.x * Time.deltaTime * 2;
        float velocityZ = +playerDirection.z * Time.deltaTime * 2;

        //print(velocityX.ToString() + " " + velocityZ.ToString());

        animationController.SetFloat("MoveX", velocityX);
        animationController.SetFloat("MoveZ", velocityZ);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, playerDirection);
    }
}
