using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLevelFinished : MonoBehaviour
{
    public GameObject rewardPanel;
    public StarsManager starsManager;
    public SplineWalker splineWalker;

    void Start()
    {

    }

    void Update()
    {
        if (splineWalker == null)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                splineWalker = GameObject.FindGameObjectWithTag("Player").GetComponent<SplineWalker>();
            }
        }
        else
        {
            //print(PlayerScript.Instance.gotCaught);
            if (splineWalker.isFinished)
            {
                Time.timeScale = 0f;
                rewardPanel.SetActive(true);

                starsManager.CollectStars(1);
               
                if (!PlayerScript.Instance.gotCaught)
                {
                    starsManager.CollectStars(2);
                }
                
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");

                if (objects.Length == 0)
                {
                    starsManager.CollectStars(3);
                }

                //if (!PlayerScript.Instance.gotCaught)
                //{
                //    starsManager.CollectStars(2);
                //}
                //else if (GameObject.FindGameObjectsWithTag("Enemy") == null)
                //{
                //    starsManager.CollectStars(3);
                //}
                //else
                //{
                //    starsManager.CollectStars(1);
                //}
            }
        }
    }
}
