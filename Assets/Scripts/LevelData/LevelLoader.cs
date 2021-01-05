using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] public LevelData levelData;

    private void Start()
    {
        Instantiate(levelData.rooms[0]);
    }
}
