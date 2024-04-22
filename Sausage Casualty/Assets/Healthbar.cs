using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider health;

    public void SetSlider(float ammount)
    {
        health.value = ammount;
    }

    public void SetSliderMax(float ammount)
    {
        health.maxValue = ammount;
        SetSlider(ammount);
    }

}
