using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 選択されているアーツセットのサイズを変更する
/// </summary>
public class ArtsDeckChoiceSizChange : MonoBehaviour
{
    List<RectTransform> childRectTransList = new List<RectTransform>();
    Vector3 changeSiz;
    Vector3 startSiz;
    int tmpNum;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform childTransform in transform)
        {
            childRectTransList.Add(childTransform.gameObject.GetComponent<RectTransform>());
        }

        startSiz = childRectTransList[0].localScale;

        changeSiz = Vector3.one * UI_Manager.GetUI_Manager.artsSelectSiz;
        childRectTransList[UI_Manager.GetChoiceArtsDeckNum].localScale = changeSiz;
    }

    // Update is called once per frame
    void Update()
    {
        if (UI_Manager.GetChoiceArtsDeckNum != tmpNum)
        {
            tmpNum = UI_Manager.GetChoiceArtsDeckNum;
            for (int i = 0; i < transform.childCount; i++)
            {
                //選択されている場合サイズを変える
                if (UI_Manager.GetChoiceArtsDeckNum == i)
                {
                    childRectTransList[i].localScale = changeSiz;
                }
                else
                {
                    childRectTransList[i].localScale = startSiz;
                }


            }
        }
    }
}
