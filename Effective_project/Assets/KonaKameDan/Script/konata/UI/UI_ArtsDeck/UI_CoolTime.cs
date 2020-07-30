using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// クールタイムUI
/// </summary>
public class UI_CoolTime : MonoBehaviour
{
    [SerializeField] int deckNum;
    string tmpId;
    string id;
    bool isStart = true;
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        id = MyArtsDeck.GetArtsDeck[deckNum].id;

        if (tmpId != id)
        {
            tmpId = id;
            isStart = false;
        }

        //クールターム表示の初期
        if (!isStart)
        {
            if (PlayerArtsInstant.coolTimes.ContainsKey(tmpId))
            {
                isStart = true;
                slider.maxValue = PlayerArtsInstant.coolTimes[tmpId];
                slider.value = PlayerArtsInstant.coolTimes[tmpId];
            }
            else slider.value = 0;
        }

        //クールターム表示
        if (slider.value != 0)
        {
            if (PlayerArtsInstant.coolTimes.ContainsKey(tmpId))
            {
                slider.value = PlayerArtsInstant.coolTimes[tmpId];
            }
            else slider.value = 0;
        }
    }
}
