using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressScript : MonoBehaviour
{
    [SerializeField] private Slider Slider;
    [SerializeField] private LevelManager levelManager;
    
    [HideInInspector] public PlayerScript Player;
    
    [SerializeField] private TextMeshProUGUI startLevelText;
    [SerializeField] private TextMeshProUGUI endLevelText;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        ViewProgress();
        SetProgressBarLevels();
    }

    void ViewProgress()
    {
        if(PlayerScript.splineWalker != null)
            Slider.value = PlayerScript.splineWalker.sliderProgress;
    }

    void SetProgressBarLevels()
    {
        startLevelText.text = (levelManager.currentLevelIndex + 1).ToString();
        endLevelText.text = (levelManager.currentLevelIndex + 2).ToString();
    }
    

}
