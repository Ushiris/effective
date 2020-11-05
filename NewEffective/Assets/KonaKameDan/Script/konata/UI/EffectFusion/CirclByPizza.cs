using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 動的にピザを作る
/// </summary>
public class CirclByPizza : MonoBehaviour
{
    [SerializeField] GameObject objPizza;
    [SerializeField] Vector3 pos;
    [SerializeField] GameObject canvas;

    float ang;
    float minAng;
    int cutNum;
    bool isStart = true;

    int tmpEffectListCount;

    List<GameObject> pizzaList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //監視オブジェクトがアクティブになった時にUIを生成する
        if (UI_Manager.GetIsEffectFusionUI_ChoiceActive)
        {
            cutNum = UI_Manager.GetUI_Manager.EffectListCount;

            //切る数を変える
            ang = 360 / cutNum;

            //ピザを円状に並べる
            if (isStart)
            {
                for (int i = 0; i < cutNum; i++)
                {
                    pizzaList.Add(Instantiate(objPizza, pos, Quaternion.AngleAxis(minAng, new Vector3(0, 0, 1))));
                    pizzaList[pizzaList.Count - 1].transform.SetParent(canvas.transform, false);
                    minAng += ang;
                }

                isStart = false;
            }

            //選択した場合色を変えるフラグ発行
            if (OnTrigger())
            {
                int num = UI_Manager.GetEffectFusionUI_ChoiceAng.num;
                PizzaObj s = pizzaList[num].transform.GetChild(0).gameObject.GetComponent<PizzaObj>();

                if (s.isChangeColor) s.isChangeColor = false;
                else if (UI_Manager.GetEffectFusionUI_ChoiceAng.numList.Count < 3)
                {
                    s.isChangeColor = true;
                }
            }
        }

        if (tmpEffectListCount != MainGameManager.GetPlEffectList.Count || !UI_Manager.GetIsEffectFusionUI_ChoiceActive)
        {
            //監視オブジェクトが非表示になった場合、UIを消す
            if (pizzaList.Count != 0)
            {
                for (int i = 0; i < pizzaList.Count; i++)
                {
                    Destroy(pizzaList[i].gameObject);
                }
                pizzaList.Clear();
            }

            isStart = true;

            tmpEffectListCount = MainGameManager.GetPlEffectList.Count;
        }
    }

    //
    bool OnTrigger()
    {
        return Input.GetKeyDown(UI_Manager.GetUI_Manager.effectFusionUI_ChoiceKey);
    }
}
