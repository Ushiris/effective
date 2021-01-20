using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エフェクト合成画面の表示系を操る
/// </summary>
public class FusionCircleManager : MonoBehaviour
{
    public class NumAndList
    {
        public int num;
        public List<int> numList;
    }

    [SerializeField] Transform circleCenter;
    [SerializeField] FusionCirclePreview fusionCirclePreview;
    [SerializeField] EffectCircleCutManager[] circleCutArr;

    //デバッグ用
    [SerializeField] int selectCount;

    public static NumAndList GetHitPosItem { get; private set; }
    public static NumAndList GetHitPosAng { get; private set; }

    string id;

    static readonly int kCutNum = 6;

    private void Awake()
    {
        //初期化
        if (GetHitPosItem == null)
        {
            GetHitPosItem = new NumAndList();
            GetHitPosItem.numList = new List<int>();
            GetHitPosAng = new NumAndList();
            GetHitPosAng.numList = new List<int>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetImage();
    }

    // Update is called once per frame
    void Update()
    {

        //選択
        selectCount = CircleCutSelectNum();
        GetHitPosAng.num = selectCount;

        //エフェクトを持っていない場合操作をさせない
        var effectId = circleCutArr[selectCount].effectId;
        var effectName = (NameDefinition.EffectName)effectId;
        if (EffectObjectAcquisition.GetEffectBag[effectName] == 0) return;

        if (EffectFusionUI_ChoiceTrigger())
        {
            var s = circleCutArr[selectCount].pieceColorControl;
            if (s.GetColorMode == FusionCircleColorControl.ColorMode.Before ||
                s.GetColorMode == FusionCircleColorControl.ColorMode.Lock)
            {
                if (GetHitPosItem.numList.Count >= 3) return;

                //データ格納
                GetHitPosItem.num = effectId;
                GetHitPosItem.numList.Add(effectId);
                GetHitPosAng.numList.Add(selectCount);

                //色替え
                s.ColorChangeAfter();
            }
            else
            {
                //データを消す
                GetHitPosItem.numList.Remove(effectId);
                GetHitPosAng.numList.Remove(selectCount);

                //色替え
                s.ColorChangeBefore();
            }

            //ArtsのID作成
            id = "";
            GetHitPosItem.numList.Sort();
            for (int i = 0; i < GetHitPosItem.numList.Count; i++)
            {
                id += GetHitPosItem.numList[i].ToString();
            }

            //中央に表示している情報を操作する
            fusionCirclePreview.ImageChange(id);
        }

        //作成したアーツを登録する
        if (ArtsEntryTrigger())
        {
            fusionCirclePreview.OnCreateArtsCheck(id);
        }
    }

    private void OnEnable()
    {
        //画像のリセット
        if (EffectObjectAcquisition.GetEffectBag != null) ResetImage();
        //for (int i = 0; i < GetHitPosAng.numList.Count; i++)
        //{
        //    var num = GetHitPosAng.numList[i];
        //    circleCutArr[num].pieceColorControl.ColorChangeBefore();
        //}
        ResetList();
        fusionCirclePreview.ImageChange("");
    }

    void ResetList()
    {
        if (GetHitPosItem != null) GetHitPosItem.numList.Clear();
        if (GetHitPosAng != null) GetHitPosAng.numList.Clear();
    }

    //画像のリセット
    void ResetImage()
    {
        for (int i = 0; i < circleCutArr.Length; i++)
        {
            var num = (NameDefinition.EffectName)circleCutArr[i].effectId;

            if (EffectObjectAcquisition.GetEffectBag[num] == 0)
            {
                circleCutArr[i].pieceColorControl.ColorChangeLock();
            }
            else
            {
                Debug.Log("Effect: " + circleCutArr[i].effectId.ToString() + " Count: " + EffectObjectAcquisition.GetEffectBag[num]);
                circleCutArr[i].pieceColorControl.ColorChangeBefore();
            }
        }
    }

    //マウスの位置から選択しているものを調べる
    int CircleCutSelectNum()
    {
        var ang = 360 / kCutNum;

        //自身の角度を取得する
        float targetAng = circleCenter.rotation.eulerAngles.z;

        //切り口の方向を調整する
        float fixAng = ang / 2;

        //マウスが上に来た時0にする
        if (targetAng <= fixAng || targetAng > fixAng + ang * (kCutNum - 1))
        {
            return 0;
        }
        else
        {
            //右から数字が始める
            for (int i = 1; i < kCutNum; i++)
            {
                if (targetAng > fixAng && targetAng <= fixAng + ang)
                {
                    return i;
                }
                fixAng += ang;
            }
        }

        return -1;
    }

    /// <summary>
    /// 選択するときのキー
    /// </summary>
    /// <returns></returns>
    bool EffectFusionUI_ChoiceTrigger()
    {
        return UI_Manager.ClickTrigger();
    }

    /// <summary>
    /// アーツを登録するButton
    /// </summary>
    /// <returns></returns>
    bool ArtsEntryTrigger()
    {
        return UI_Manager.ArtsEntryTrigger();
    }
}
