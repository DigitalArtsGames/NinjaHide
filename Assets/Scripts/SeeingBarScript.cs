using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SeeingBarScript : MonoBehaviour
{
    public Slider barSlider;

    public void SetMaxSeeingValue(int maxSeeingValue)
    {
        barSlider.maxValue = maxSeeingValue;
        barSlider.value = maxSeeingValue;
    }

    public void SetSeeingValue(int seeingValue)
    {
        barSlider.value = seeingValue;
    }

    public float GetMaxValue()
    {
        return barSlider.maxValue;
    }
}
