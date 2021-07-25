using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkBar : MonoBehaviour
{
    public static DrinkBar instance;

    [SerializeField] private Slider slider;
    public float drinkLeft;

    private void Awake()
    {
        instance = this;
    }

    public void SetMaxAmount(int value)
    {
        slider.maxValue = value;
        slider.value = value;
        drinkLeft = value;
    }    

    public void Drink(int value)
    {
        drinkLeft -= value;
        slider.value = drinkLeft;
    }
}
