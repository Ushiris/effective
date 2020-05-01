using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtsUI_Instant : MonoBehaviour
{
    [SerializeField] GameObject artsDeckObj;
    List<GameObject> childObj = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //子を格納
        foreach (Transform childTransform in transform)
        {
            childObj.Add(childTransform.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OnTrigger())
        {
            //子が2である場合生成、違う場合破壊
            int count = childObj[UI_Manager.GetChoiceArtsDeckNum].transform.childCount;
            if (count == 2)
            {
                foreach (Transform childTransform in childObj[UI_Manager.GetChoiceArtsDeckNum].transform)
                {
                    //アーツセットUIを見つけ破壊
                    if (childTransform.tag == "ArtsDeckUI")
                    {
                        Destroy(childTransform.gameObject);
                        break;
                    }
                }
            }
            else
            {
                //生成
                GameObject obj = Instantiate(artsDeckObj);
                obj.transform.SetParent(childObj[UI_Manager.GetChoiceArtsDeckNum].transform, false);
            }
        }
    }

    //
    bool OnTrigger()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }
}
