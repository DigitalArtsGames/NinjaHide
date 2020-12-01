using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public float runSpeed = 10f;
    public float crouchSpeed = 5f;
    private Transform target;
    private int waypointIndex = 0;
    void Start()
    {
        target = WaypointsScript.waypoints[0];
    }

    void Update()
    {
        Crouch();
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= WaypointsScript.waypoints.Length - 1)
        {
            Debug.Log("WIN!!");
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = WaypointsScript.waypoints[waypointIndex];
    }

    void Crouch()
    {
        if(Input.GetButton("Crouch"))
        {
            speed = crouchSpeed;
        }
        else
        {
            speed = runSpeed;
        }
    }
}
