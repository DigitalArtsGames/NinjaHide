using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressScript : MonoBehaviour
{
    [HideInInspector] public PlayerScript Player;
    
    [SerializeField] private Slider Slider;


    private void Start()
    {

    }

    private void Update()
    {
        if(PlayerScript.splineWalker != null)
            Slider.value = PlayerScript.splineWalker.progress;
    }
}
