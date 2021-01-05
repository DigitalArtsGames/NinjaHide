using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Darts Game/Level Data")]
public class TestLevel : ScriptableObject
{
    public Room[] levelRooms;
}
