using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLevelFinished : MonoBehaviour
{
    public GameObject rewardPanel;
    public StarsManager starsManager;
    public SplineWalker splineWalker;

    [HideInInspector] public bool isRewardMenuOpened;

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
            ManageRewardMenu();
        }
    }


    void ManageRewardMenu()
    {
        if(splineWalker.isFinished && !isRewardMenuOpened)
        {
            rewardPanel.SetActive(true);
            Time.timeScale = 0f;
            ManageStars();
            isRewardMenuOpened = true;
        }
    }

    void ManageStars()
    {
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
    }
}
