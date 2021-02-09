using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLevelFinished : MonoBehaviour
{
    public GameObject rewardPanel;
    public StarsManager starsManager;
    public SplineWalker splineWalker;

    public delegate void IsFinischedSplineDelegate();
    public event IsFinischedSplineDelegate FinishedSplineEvent;

    public LevelManager levelManager;

    void Start()
    {
        FinishedSplineEvent += ManageRewardMenu;
    }

    void Update()
    {
        CheckIsFinishedSpline();
        print(levelManager.currentLevelIndex);
        GetSplineWalker();
        //print(isRewardMenuOpened);
        //IsEnable();
    }

    public void GetSplineWalker()
    {
        if (splineWalker == null)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                splineWalker = GameObject.FindGameObjectWithTag("Player").GetComponent<SplineWalker>();
            }
        }
    }

    public void CheckIsFinishedSpline()
    {
        if (splineWalker != null && splineWalker.isFinished && FinishedSplineEvent != null)
        {
            FinishedSplineEvent();
        }
    }

    void ManageRewardMenu()
    {
        if (splineWalker != null)
        {
            rewardPanel.SetActive(true);
            //Player законченного уровня удаляется чтобы, быть созданным в следующем (т.к. Плейер является статиком)
            //Destroy(GameObject.FindGameObjectWithTag("Player"));
            Time.timeScale = 0f;
            ManageStars();
            levelManager.currentLevelIndex++;
        }
        FinishedSplineEvent -= ManageRewardMenu;
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
