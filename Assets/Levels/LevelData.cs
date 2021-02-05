using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "DartsGame/Level Data")]
public class LevelData : ScriptableObject
{
    public new string name;
    public Room[] rooms;
    private List<Room> spawnedRooms;
}
