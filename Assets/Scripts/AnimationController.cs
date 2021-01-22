using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animationController;
    [SerializeField] private SplineWalker splineWalker;
    [SerializeField] private PlayerScript playerScript;

    float velocityX;
    float velocityZ;

    private Vector3 playerDirection;
    private void Update()
    {
        ManageAnimation();
    }

    void ManageAnimation()
    {

        //print(playerScript.isHiding);
        //playerDirection = PlayerScript.splineWalker.GetPlayerDirection();
        if (playerScript.GetTarget(10) != null && !playerScript.isHiding)
        {
            playerDirection = playerScript.GetPlayerDirection(10) * 15;
            
            //Debug.Log("Direction to Enemy!");
        }
        else if (playerScript.isHiding)
        {
            playerDirection = playerScript.GetHidingSpotDirection();
            playerScript.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            playerDirection = transform.rotation * Vector3.right;
            playerDirection = transform.InverseTransformDirection(playerDirection);
        }

        //float velocityX = playerDirection.x * Time.deltaTime * 1000;
        //float velocityZ = playerDirection.z * Time.deltaTime * 1000;

        //print(velocityX.ToString() + " " + velocityZ.ToString());

        playerDirection = Vector3.Normalize(playerDirection);

        velocityX = playerDirection.x;
        velocityZ = playerDirection.z;


        animationController.SetFloat("MoveX", velocityX);
        animationController.SetFloat("MoveZ", velocityZ);


    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(transform.position, playerDirection);
    }
}
