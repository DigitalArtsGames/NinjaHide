using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform playerSpawnPoint;

    public PlayerScript player;

    public void SpawnPlayer()
    {
        player.transform.position = playerSpawnPoint.position;
        player = Instantiate(player);
    }

    public SplineWalker GetSplineWalker()
    {
        return player.GetComponent<SplineWalker>();
    }

    public void SetSplineWalkerActive(bool isActive)
    {
        player.GetComponent<SplineWalker>().enabled = isActive;
    }

    public void SetSplineManager(SplineManager splineManager)
    {
        player.GetComponent<SplineWalker>().splineManager = splineManager;
    }

}
