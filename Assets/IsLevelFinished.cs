using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLevelFinished : MonoBehaviour
{
    public GameObject rewardPanel;
    public StarsManager starsManager;
    //public SplineWalker splineWalker;

    void Start()
    {

    }

    void Update()
    {
        if(SplineWalker.Instance.isFinished)
        {
            rewardPanel.SetActive(true);
            starsManager.CollectStars(3);
        }
    }
}
