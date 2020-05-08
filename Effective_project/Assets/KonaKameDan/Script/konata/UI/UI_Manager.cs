using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIのマネージャー
/// </summary>
public class UI_Manager : MonoBehaviour
{
    [Header("アーツを作るUI表示")]
    [SerializeField] KeyCode effectFusionUI_ActiveKey = KeyCode.E;
    public GameObject effectFusionUI_Obj;
    //[SerializeField] GameObject effectFusionUI_CircleMakeByPizza;

    [SerializeField] GameObject textObj;

    [Header("アーツを登録するキー")]
    public KeyCode artsEntryKey = KeyCode.Q;

    [Header("アーツを選択するキー")]
    public KeyCode effectFusionUI_ChoiceKey = KeyCode.Mouse0;

    [Header("アーツセット表示タイミング")]
    public float artsSetTimingTnterval = 0.1f;

    [Header("アーツ表示UIサイズ")]
    //public float artsDeckDefaultSiz = 0.5f;
    public float artsSelectSiz = 0.6f;

    [Header("アーツ表示UI間の幅")]
    public float artsDeckSpace = 80f;

    [Header("エフェクトの最大の数")]
    public int EffectListCount = 10;
    [SerializeField] bool onEffectListCauntDebug;

    public static UI_Manager GetUI_Manager;

    void Awake()
    {
        //所持エフェクト
        if (!onEffectListCauntDebug) EffectListCount = MainGameManager.GetPlEffectList.Count;

        GetUI_Manager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //所持エフェクト
        if (!onEffectListCauntDebug)
        {
            EffectListCount = MainGameManager.GetPlEffectList.Count;
        }

        EffectFusionUI();


        
    }

    void EffectFusionUI()
    {
        //アーツを作るUI表示
        if (EffectFusionUI_ActiveTrigger() && EffectListCount != 0)
        {
            if (!effectFusionUI_Obj.activeSelf)
            {
                effectFusionUI_Obj.SetActive(true);
                //effectFusionUI_CircleMakeByPizza.SetActive(true);
                textObj.SetActive(true);
            }
            else
            {
                effectFusionUI_Obj.SetActive(false);
                //effectFusionUI_CircleMakeByPizza.SetActive(false);
                textObj.SetActive(false);
            }
        }
    }

    /// <summary>
    /// アーツを作るUI表示キー
    /// </summary>
    /// <returns></returns>
    bool EffectFusionUI_ActiveTrigger()
    {
        return Input.GetKeyDown(effectFusionUI_ActiveKey);
    }

    /// <summary>
    /// アーツを登録するボタン
    /// </summary>
    /// <returns></returns>
    public static bool ArtsEntryTrigger()
    {
        return Input.GetKeyDown(GetUI_Manager.artsEntryKey);
    }

    /// <summary>
    /// エフェクト選択メニュー画面がアクティブかどうか
    /// </summary>
    public static bool GetIsEffectFusionUI_ChoiceActive
    {
        get { return GetUI_Manager.effectFusionUI_Obj.activeSelf; }
    }

    /// <summary>
    /// 選択したエフェクト
    /// </summary>
    public static EffectFusionUi.NumAndList GetEffectFusionUI_ChoiceNum
    {
        get { return EffectFusionUi.GetHitPosItem; }
    }

    /// <summary>
    /// 選択したエフェクトの角度
    /// </summary>
    public static EffectFusionUi.NumAndList GetEffectFusionUI_ChoiceAng
    {
        get { return EffectFusionUi.GetHitPosAng; }
    }

    /// <summary>
    /// 選択したアーツセット
    /// </summary>
    /// <returns></returns>
    public static int GetChoiceArtsDeckNum
    {
        get { return ArtsDeckChangeControll.GetNum; }
    }
}
