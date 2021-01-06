using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTriggerScript : MonoBehaviour
{
    private LevelLoader levelLoader;

    private void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelLoader>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(levelLoader != null)
            {
                levelLoader.LoadNextLevel();
            }
        }
    }
}
