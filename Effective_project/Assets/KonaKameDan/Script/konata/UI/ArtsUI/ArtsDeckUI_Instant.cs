using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アーツ表示1つのセットを生成
/// </summary>
public class ArtsDeckUI_Instant : MonoBehaviour
{
    [SerializeField] GameObject artsIcon;
    [SerializeField] GameObject artsText;
    [SerializeField] float space = 10f;

    public int artsCount;

    //渡すことのできる
    public List<GameObject> artsIconList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        artsCount = UI_Manager.GetEffectFusionUI_ChoiceList.Count;

        //オブジェクトの幅を取得
        float fixSpace = artsText.GetComponent<RectTransform>().sizeDelta.x;
        float iconWidth = artsIcon.GetComponent<RectTransform>().sizeDelta.x;

        //アーツ名表示用テキストの生成
        InstantObj(artsText, fixSpace / 2 - iconWidth / 2, artsCount + 2);

        float f = fixSpace / 2 + space;
        fixSpace = artsIconList[0].GetComponent<RectTransform>().localPosition.x + f;

        //アーツアイコン生成
        for (int i = 1; i < artsCount + 1; i++)
        {
            InstantObj(artsIcon, fixSpace, artsCount - i);

            //位置の調整
            fixSpace = artsIconList[i].GetComponent<RectTransform>().localPosition.x + iconWidth / 2;
            fixSpace += space;
        }

        //画像差し替え
        for (int i = 0; i < UI_Manager.GetEffectFusionUI_ChoiceList.Count; i++)
        {
            int num = UI_Manager.GetEffectFusionUI_ChoiceList[i];
            if (UI_Image.GetUI_Image.effectIconList.Count > num)
            {
                artsIconList[i + 1].GetComponent<Image>().sprite = UI_Image.GetUI_Image.effectIconList[num];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 生成
    /// </summary>
    /// <param name="instantObj">生成するもの</param>
    /// <param name="objSpace">オブジェクト間の調整</param>
    /// <param name="popInterval">アニメーションのタイミング</param>
    void InstantObj(GameObject instantObj, float objSpace, float popInterval)
    {
        //生成
        artsIconList.Add(
               Instantiate(instantObj, new Vector3(objSpace, 0, 0), new Quaternion()));

        //子にする
        artsIconList[artsIconList.Count - 1].transform.SetParent(transform, false);

        //アニメーション制御
        artsIconList[artsIconList.Count - 1].GetComponent<ArtsMoveIcon>().interval =
            UI_Manager.GetUI_Manager.artsSetTimingTnterval * popInterval;
    }
}
