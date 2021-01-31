using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelComponent : MonoBehaviour
{
    public TextMeshProUGUI TextMeshProUGUI;
    
    public void SetText(string text)
    {
        TextMeshProUGUI.text = text;
    }

}
