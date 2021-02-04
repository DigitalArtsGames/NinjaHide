using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsContainer", menuName = "DartsGame/Levels Container")]
public class LevelsCollection : ScriptableObject
{
    public new string name;
    public LevelData[] levels;
}
