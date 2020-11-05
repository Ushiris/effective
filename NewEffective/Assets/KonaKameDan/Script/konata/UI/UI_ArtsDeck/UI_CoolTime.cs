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
        if (isStart)
        {
            //float time = Time.deltaTime * 100;
            slider.value -= Time.deltaTime;
            if (slider.value < 0) isStart = false;
        }
    }

    /// <summary>
    /// クールタイムの初期値代入
    /// </summary>
    /// <param name="time">クールタイムの経過時間</param>
    /// <param name="max">クールタイムのmax値</param>
    public void SetCoolTime(float time,float max)
    {
        slider.maxValue = max;
        slider.value = time;
        isStart = true;
    }
}
