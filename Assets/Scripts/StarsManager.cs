using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsManager : MonoBehaviour
{
    public GameObject[] enabledStars;
    public GameObject[] disabledStars;

    public void ResetStars()
    {
        for (int i = 0; i < 3; i++)
        {
            enabledStars[i].SetActive(false);
        }
    }

    public void CollectStars(int wonStarsCount)
    {
        if(wonStarsCount <= 3 || wonStarsCount >= 0)
        {
            ResetStars();
            for (int i = 0; i < wonStarsCount; i++)
            {
                enabledStars[i].SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("There are too many stars. Max stars is 3.");
        }
    }
}
