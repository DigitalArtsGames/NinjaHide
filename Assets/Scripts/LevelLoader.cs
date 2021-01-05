using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public TestLevel levelData;

    private void Start()
    {
        Instantiate(levelData.levelRooms[0]);
    }
}
