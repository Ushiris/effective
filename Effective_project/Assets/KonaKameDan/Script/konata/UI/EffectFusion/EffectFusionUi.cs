using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自身の角度をもとに自身から見たマウスの角度を取得する
/// </summary>
public class EffectFusionUi : MonoBehaviour
{
    int cutNum = 10;
    float ang;

    public class NumAndList
    {
        public int num;
        public List<int> numList;
    }
    public static NumAndList GetHitPosItem { get; private set; }
    public static NumAndList GetHitPosAng { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        GetHitPosItem = new NumAndList();
        GetHitPosItem.numList = new List<int>();
        GetHitPosAng = new NumAndList();
        GetHitPosAng.numList = new List<int>();

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ResetList();
    }

    // Update is called once per frame
    void Update()
    {
        cutNum = UI_Manager.GetUI_Manager.EffectListCount;

        //切る数を変える
        ang = 360 / cutNum;

        ChoiceNum();


        //所持しているエフェクトからidを持ってくる
        if (MainGameManager.GetPlEffectList.Count != 0)
        {
            GetHitPosItem.num = MainGameManager.GetPlEffectList[GetHitPosAng.num].id;
        }
        else
        {
            GetHitPosItem.num = GetHitPosAng.num;
        }

        if (EffectFusionUI_ChoiceTrigger())
        {
            ChoiceList(GetHitPosItem);
            ChoiceList(GetHitPosAng);
        }
    }

    void ResetList()
    {
        if (GetHitPosItem != null) GetHitPosItem.numList.Clear();
        if (GetHitPosAng != null) GetHitPosAng.numList.Clear();
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
            GetHitPosAng.num = 0;
        }
        else
        {
            //右から数字が始める
            for (int i = 1; i < cutNum; i++)
            {
                if (targetAng > minAng && targetAng <= minAng + ang)
                {
                    GetHitPosAng.num = i;
                    break;
                }
                minAng += ang;
            }
        }
    }

    //選択したものをリストに格納する
    void ChoiceList(NumAndList item)
    {
        if (item.numList.Contains(item.num))
        {
            //同じものを選択した場合、それを消す
            item.numList.Remove(item.num);
        }
        else
        {
            if (item.numList.Count < 3)
            {
                //リスト内に選択したものがない場合リストに格納する
                item.numList.Add(item.num);
            }
        }
    }

    /// <summary>
    /// 選択するときのキー
    /// </summary>
    /// <returns></returns>
    bool EffectFusionUI_ChoiceTrigger()
    {
        return Input.GetKeyDown(UI_Manager.GetUI_Manager.effectFusionUI_ChoiceKey);
    }
}
