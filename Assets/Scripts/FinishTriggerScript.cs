using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTriggerScript : MonoBehaviour
{
    private LevelManager levelLoader;

    private void Start()
    {
        
        levelLoader = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    private void Update()
    {
        //FinishedPath();
    }

    //void FinishedPath()
    //{
    //    if (PlayerScript.splineWalker.progress == 1)
    //    {
    //        levelLoader.LoadRewardMenu();
    //    }
    //}
}
