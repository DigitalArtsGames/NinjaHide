using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] public LevelData levelData;

    private void Update()
    {
        Debug.Log(levelData.rooms[0].player.tag);
    }
}
