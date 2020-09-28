using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIのマネージャー
/// </summary>
public class UI_Manager : MonoBehaviour
{
    [Header("アーツを作るUI表示")]
    public KeyCode effectFusionUI_ActiveKey = KeyCode.E;
    public GameObject effectFusionUI_Obj;
    //[SerializeField] GameObject effectFusionUI_CircleMakeByPizza;

    [SerializeField] GameObject textObj;
    [SerializeField] GameObject cameraControlObj;

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
    int tmpEffectListCount = 0;

    void Awake()
    {
        //所持エフェクト
        if (!onEffectListCauntDebug) EffectListCount = MainGameManager.GetPlEffectList.Count;
        GetUI_Manager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined; //カーソルをウィンドウ内に
        Cursor.visible = false;

        cameraControlObj = GameObject.FindGameObjectWithTag("CameraPivot");
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
        //PizzaReset();
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

                //カメラ操作を切る
                if (cameraControlObj != null)
                    cameraControlObj.GetComponent<TpsPlayerControl>().enabled = false;

                //マウスカーソルを表示
                Cursor.visible = true;
            }
            else
            {
                effectFusionUI_Obj.SetActive(false);
                //effectFusionUI_CircleMakeByPizza.SetActive(false);
                textObj.SetActive(false);

                //カメラ操作を復活させる
                cameraControlObj.GetComponent<TpsPlayerControl>().enabled = true;

                //マウスカーソルを非表示
                Cursor.visible = false;
            }
        }
    }

    //ピザのリセット
    void PizzaReset()
    {
        if (GetUI_Manager.effectFusionUI_Obj.activeSelf)
        {
            if (tmpEffectListCount != EffectListCount)
            {
                effectFusionUI_Obj.SetActive(false);
                tmpEffectListCount = EffectListCount;
            }
        }
    }

    /// <summary>
    /// アーツを作るUI表示キー
    /// </summary>
    /// <returns></returns>
    bool EffectFusionUI_ActiveTrigger()
    {
        return Time.timeScale > 0.1f && Input.GetKeyDown(GetUI_Manager.effectFusionUI_ActiveKey);
    }

    /// <summary>
    /// アーツを登録するボタン
    /// </summary>
    /// <returns></returns>
    public static bool ArtsEntryTrigger()
    {
        return Time.timeScale > 0.1f && Input.GetKeyDown(GetUI_Manager.artsEntryKey);
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
