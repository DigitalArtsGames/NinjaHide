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
        if(SplineWalker.Instance != null)
        {
            print(PlayerScript.Instance.gotCaught);
            if (SplineWalker.Instance.isFinished)
            {
                rewardPanel.SetActive(true);
            
                if (!PlayerScript.Instance.gotCaught)
                {
                    starsManager.CollectStars(2);
                }
                else if (GameObject.FindGameObjectsWithTag("Enemy") == null)
                {
                    starsManager.CollectStars(3);
                }
                else
                {
                    starsManager.CollectStars(1);
                }
            }
        }
    }
}
