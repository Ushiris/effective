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
    [SerializeField] GameObject EffectFusionUiObj;
    [SerializeField] Vector3 pos;
    [SerializeField] GameObject canvas;

    float ang;
    float minAng;
    int cutNum;
    bool isStart = true;

    List<GameObject> pizzaList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EffectFusionUiObj.activeSelf)
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

                isStart = false;    //監視オブジェクトがアクティブになった時にUIを生成する
            }

            for (int i = 0; i < cutNum; i++)
            {
                //選択した場合色を変える
                if (UI_Manager.GetEffectFusionUI_ChoiceList().Contains(i))
                {
                    Color color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.5f);
                    pizzaList[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = color;
                }
                else
                {
                    Color color = new Color(Color.black.r, Color.black.g, Color.black.b, 0.5f);
                    pizzaList[i].transform.GetChild(0).gameObject.GetComponent<Image>().color =color;
                }
            }
        }
        else
        {
            //監視オブジェクトが非表示になった場合、UIを消す
            if (pizzaList.Count != 0)
            {
                for (int i = 0; i < pizzaList.Count; i++)
                {
                    Destroy(pizzaList[i].gameObject);
                    pizzaList.RemoveAt(i);
                }
            }

            isStart = true;
        }
    }
}
