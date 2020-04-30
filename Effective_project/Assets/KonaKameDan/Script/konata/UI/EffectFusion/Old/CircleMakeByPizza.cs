using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アーツを作成するUIの表示
/// </summary>
public class CircleMakeByPizza : MonoBehaviour
{
    [SerializeField] Image onPizzaImage;
    [SerializeField] Image offPizzaImage;
    [SerializeField] Image voidPizzaImage;

    [SerializeField] GameObject ObjPizza;

    [SerializeField] Vector3 pos;
    [SerializeField] GameObject canvas;

    float ang;
    float minAng;
    int cutNum;

    List<GameObject> pizzaList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        cutNum = UI_Manager.GetUI_Manager.EffectListCount;

        //切る数を変える
        ang = 360 / cutNum;

        //ピザを円状に並べる
        for (int i = 0; i < cutNum; i++)
        {
            pizzaList.Add(Instantiate(ObjPizza, pos, Quaternion.Euler(0, 0, minAng)));
            pizzaList[pizzaList.Count - 1].transform.SetParent(canvas.transform, false);
            minAng += ang;
        }
    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cutNum; i++)
        {
            if (PossessionEffectList.GetIsPossessionEffect(i))
            {
                //選択した場合色を変える
                if (UI_Manager.GetEffectFusionUI_ChoiceList.Contains(i))
                {
                    pizzaList[i].GetComponent<Image>().sprite = onPizzaImage.sprite;
                }
                else
                {
                    pizzaList[i].GetComponent<Image>().sprite = offPizzaImage.sprite;
                }
            }
            else
            {
                pizzaList[i].GetComponent<Image>().sprite = voidPizzaImage.sprite;
            }
        }
    }
}
