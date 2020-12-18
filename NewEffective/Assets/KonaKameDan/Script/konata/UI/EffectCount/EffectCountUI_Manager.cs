using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCountUI_Manager : MonoBehaviour
{
    [SerializeField] GameObject instantEffectIconUI;
    [SerializeField] float w=100;
    [SerializeField] float h=100;
    [SerializeField] int maxCount;

    int effectListCount = 0;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i< MainGameManager.GetPlEffectList.Count; i++)
        {
            InstantUI(i);
            effectListCount = MainGameManager.GetPlEffectList.Count;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (MainGameManager.GetPlEffectList.Count != effectListCount)
        {
            for (int i = effectListCount; i < MainGameManager.GetPlEffectList.Count; i++)
            {
                InstantUI(i);
            }
            effectListCount = MainGameManager.GetPlEffectList.Count;
        }
    }

    void InstantUI(int count)
    {
        int effectImageNum = MainGameManager.GetPlEffectList[count].id;

        GameObject UiObj = Instantiate(instantEffectIconUI, transform);
        UiObj.GetComponent<RectTransform>().localPosition = pos;

        UiObj.GetComponentInChildren<EffectIconDisplay>().num = effectImageNum;
        UiObj.GetComponentInChildren<EffectCountTMPro>().num = count;

        if (count == maxCount)
        {
            pos.x -= w;
            pos.y = 0;
        }
        else pos.y -= h;
    }
}
