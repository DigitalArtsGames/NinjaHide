using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLevelFinished : MonoBehaviour
{
    public GameObject rewardPanel;
    public StarsManager starsManager;
    public SplineWalker splineWalker;

    public LevelManager levelManager;

    [HideInInspector] public bool isRewardMenuOpened;

    void Start()
    {

    }

    void Update()
    {
        //print(isRewardMenuOpened);
        //IsEnable();
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

    public void ClearBoolean()
    {
        isRewardMenuOpened = false;
    }

    void ManageRewardMenu()
    {
        if(splineWalker.isFinished && !isRewardMenuOpened)
        {
            rewardPanel.SetActive(true);
            //Player законченного уровня удаляется чтобы, быть созданным в следующем (т.к. Плейер является статиком)
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            Time.timeScale = 0f;
            ManageStars();
            levelManager.currentLevelIndex++;
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
