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
        
    }

    // Update is called once per frame
    void Update()
    {

        if (MainGameManager.GetPlEffectList.Count != effectListCount)
        {
            int effectImageNum = MainGameManager.GetPlEffectList[effectListCount].id;

            GameObject UiObj = Instantiate(instantEffectIconUI, transform);
            UiObj.GetComponent<RectTransform>().localPosition = pos;

            UiObj.GetComponentInChildren<EffectIconDisplay>().num = effectImageNum;
            UiObj.GetComponentInChildren<EffectCountTMPro>().num = effectListCount;

            if (effectListCount == maxCount)
            {
                pos.x -= w;
                pos.y = 0;
            }
            else pos.y -= h;

            effectListCount = MainGameManager.GetPlEffectList.Count;
        }
    }
}
