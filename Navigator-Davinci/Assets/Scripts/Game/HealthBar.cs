using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    int Currenthealth;
    int Maxhealth;

    public void Update()
    {

        GameObject.Find("Healthpoints").GetComponent<TextMeshProUGUI>().text = Maxhealth + " / " + Currenthealth;
    }


    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        Currenthealth = health;

    }

    public void SetHealth(int health)
    {
        slider.value = health;
        Maxhealth = health;
    }



}
