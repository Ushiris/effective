using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// クールタイムUI
/// </summary>
public class UI_CoolTime : MonoBehaviour
{
    bool isStart;
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (isStart)
        //{
        //    //float time = Time.deltaTime * 100;
        //    slider.value -= Time.deltaTime;
        //    if (slider.value < 0) isStart = false;
        //}
    }

    /// <summary>
    /// クールタイムセット
    /// </summary>
    /// <param name="max"></param>
    public void SetMaxTime(float max)
    {
        slider.maxValue = max;
        slider.value = max;
        isStart = true;
    }

    public void SetTimeMax(float max)
    {
        slider.maxValue = max;
    }

    public void SetTime(float time)
    {
        slider.value = time;
    }
}
