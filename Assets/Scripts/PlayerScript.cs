using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float runSpeed = 10f;
    public float crouchSpeed = 5f;
    
    private float speed;
    private Transform waypoint;
    private int waypointIndex = 0;
    //private SphereCollider sphereCollider;
    void Start()
    {
        //sphereCollider = GetComponent<SphereCollider>();
        waypoint = WaypointsScript.waypoints[0];
    }

    void Update()
    {
        Crouch();
        ToWaypointsMover();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("ENEMY!");
        }
    }

    void ToWaypointsMover()
    {
        Vector3 dir = waypoint.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, waypoint.position) <= 0.2f)
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
        waypoint = WaypointsScript.waypoints[waypointIndex];
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
