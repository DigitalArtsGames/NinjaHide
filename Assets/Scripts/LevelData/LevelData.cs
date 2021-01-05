using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "DartsGame/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] public string name;
    [SerializeField] public Room[] rooms;
}
