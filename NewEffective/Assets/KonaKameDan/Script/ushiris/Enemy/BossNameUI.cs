using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossNameUI : MonoBehaviour
{
    public void Generate(Slider indicator, Canvas parent)
    {
        var slider = gameObject.GetComponentInChildren<Slider>();
        slider.maxValue = indicator.maxValue;
        slider.minValue = indicator.minValue;
        slider.value = indicator.value;
    }

    public void SetName(string name)
    {
        GetComponent<Text>().text = name;
    }
}
