using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 自身の角度をもとに自身から見たマウスの角度を取得する
/// </summary>
public class EffectFusionUi : MonoBehaviour
{
    [SerializeField] KeyCode effectFusionUI_ChoiceKey = KeyCode.Mouse0;

    int cutNum = 10;
    float ang;

    public static int GetHitPosNum { get; private set; }
    public static List<int> GetEffectFusionList { get; private set; }

    //デバッグ用
    //[SerializeField] int debugNum;
    //[SerializeField] List<int> debugNumList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        GetEffectFusionList = new List<int>();
        GetHitPosNum = 0;

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cutNum = UI_Manager.GetUI_Manager.EffectListCount;

        //切る数を変える
        ang = 360 / cutNum;

        ChoiceNum();
        ChoiceList();

        //デバッグ
        //debugNum = GetHitPosNum;
        //debugNumList = GetEffectFusionList;
    }

    //角度を決める
    void ChoiceNum()
    {
        //自身の角度を取得する
        float targetAng = transform.rotation.eulerAngles.z;

        //切り口の方向を調整する
        float minAng = ang / 2;

        //マウスが上に来た時0にする
        if (targetAng <= minAng || targetAng > minAng + ang * (cutNum - 1))
        {
            GetHitPosNum = 0;
        }
        else
        {
            //右から数字が始める
            for (int i = 1; i < cutNum; i++)
            {
                if (targetAng > minAng && targetAng <= minAng + ang)
                {
                    GetHitPosNum = i;
                    break;
                }
                minAng += ang;
            }
        }
    }

    //選択したものをリストに格納する
    void ChoiceList()
    {
        if (EffectFusionUI_ChoiceTrigger())
        {
            if (GetEffectFusionList.Contains(GetHitPosNum))
            {
                //同じものを選択した場合、それを消す
                GetEffectFusionList.Remove(GetHitPosNum);
            }
            else
            {
                if (GetEffectFusionList.Count < 3)
                {
                    //リスト内に選択したものがない場合リストに格納する
                    GetEffectFusionList.Add(GetHitPosNum);
                }
            }
        }
    }

    /// <summary>
    /// 選択するときのキー
    /// </summary>
    /// <returns></returns>
    bool EffectFusionUI_ChoiceTrigger()
    {
        return Input.GetKeyDown(effectFusionUI_ChoiceKey);
    }
}
