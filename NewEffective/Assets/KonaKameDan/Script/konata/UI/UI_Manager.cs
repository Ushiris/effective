﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// UIのマネージャー
/// </summary>
public class UI_Manager : MonoBehaviour
{
    public GameObject uICanvas;
    public GameObject artsPlateCanvas;

    [Header("アーツを作るUI表示")]
    public KeyCode effectFusionUI_ActiveKey = KeyCode.E;
    public GameObject effectFusionUI_Obj;
    //[SerializeField] GameObject effectFusionUI_CircleMakeByPizza;

    [SerializeField] GameObject textObj;
    [SerializeField] GameObject cameraControlObj;
    [SerializeField] Image reticle;

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
    delegate void Action();

    AudioSource artsFusionMenuSe;

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

        SceneManager.sceneUnloaded += FixFixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {

        //所持エフェクト
        if (!onEffectListCauntDebug)
        {
            EffectListCount = MainGameManager.GetPlEffectList.Count;
        }

        //アーツを作るUI表示
        if (EffectFusionUI_ActiveTrigger() && EffectListCount != 0)
        {
            EffectFusionUI();
        }
        //PizzaReset();

        //Artsを登録した後にUIを消す
        if (ArtsEntryTrigger())
        {
            StartCoroutine(FrameWait(() => EffectFusionUI(), 1));
        }
    }

    //アーツを作るUI表示
    void EffectFusionUI()
    {
        if (!effectFusionUI_Obj.activeSelf)
        {
            //SE
            artsFusionMenuSe = SE_Manager.SePlay(SE_Manager.SE_NAME.SlowMotion);

            effectFusionUI_Obj.SetActive(true);
            //effectFusionUI_CircleMakeByPizza.SetActive(true);
            textObj.SetActive(true);

            //カメラ操作を切る
            if (cameraControlObj != null)
                cameraControlObj.GetComponent<TpsPlayerControl>().enabled = false;

            //マウスカーソルを表示
            Cursor.visible = true;

            //レティクルを非表示
            reticle.enabled = false;

            Time.timeScale = 0.3f;
            Time.fixedDeltaTime *= 0.3f;
        }
        else
        {
            if (artsFusionMenuSe != null)
            {
                SE_Manager.SetFadeOut(this, artsFusionMenuSe, 3);
            }

            effectFusionUI_Obj.SetActive(false);
            //effectFusionUI_CircleMakeByPizza.SetActive(false);
            textObj.SetActive(false);

            //カメラ操作を復活させる
            cameraControlObj.GetComponent<TpsPlayerControl>().enabled = true;

            //マウスカーソルを非表示
            Cursor.visible = false;

            //レティクルを表示
            reticle.enabled = true;
            FixFixedDeltaTime();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        FixFixedDeltaTime();
    }
    public static void FixFixedDeltaTime(Scene scene)
    {
        Time.fixedDeltaTime = .02f;
        Time.timeScale = 1.0f;
    }

    public static void FixFixedDeltaTime()
    {
        Time.fixedDeltaTime = .02f;
        Time.timeScale = 1.0f;
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

    //〇〇フレーム遅延させるやつ
    IEnumerator FrameWait(Action action, int frameCount)
    {
        for(int i = 0; i < frameCount; i++)
        {
            yield return null;
        }
        action();
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

    public static bool ClickTrigger()
    {
        var on = Time.timeScale > 0.1f && Input.GetMouseButtonDown(0);
        if (on) SE_Manager.SePlay(SE_Manager.SE_NAME.Menu_Change);
        return on;

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
    public static FusionCircleManager.NumAndList GetEffectFusionUI_ChoiceNum
    {
        get { return FusionCircleManager.GetHitPosItem; }
    }

    /// <summary>
    /// 選択したエフェクトの角度
    /// </summary>
    public static FusionCircleManager.NumAndList GetEffectFusionUI_ChoiceAng
    {
        get { return FusionCircleManager.GetHitPosAng; }
    }

    /// <summary>
    /// 選択したアーツセット
    /// </summary>
    /// <returns></returns>
    public static int GetChoiceArtsDeckNum
    {
        get { return ArtsDeckChangeControll.GetNum; }
    }

    /// <summary>
    /// メインUIのCanvasを持ってくる
    /// </summary>
    public static Canvas GetMainUiCanvas
    {
        get { return GetUI_Manager.uICanvas.GetComponent<Canvas>(); }
    }

    /// <summary>
    /// 表示しているUIの表示・非表示
    /// </summary>
    /// <param name="isEnable"></param>
    public static void OnUIEnable(bool isEnable)
    {
        GetUI_Manager.uICanvas.GetComponent<Canvas>().enabled = isEnable;
        GetUI_Manager.artsPlateCanvas.GetComponent<Canvas>().enabled = isEnable;
        GetUI_Manager.effectFusionUI_Obj.GetComponent<Canvas>().enabled = isEnable;
    }

    /*  SE  */
    #region　SEを流す時に呼ばれる関数定義

    /// <summary>
    /// エフェクトアイコンが出現するときに流れるSE
    /// </summary>
    public static void EffectIconSetPlaySe()
    {
    }

    /// <summary>
    /// エフェクトがセットされるときに流れるSE
    /// </summary>
    public static void EffectIconFrameSetPlaySe()
    {
        SE_Manager.SePlay(SE_Manager.SE_NAME.EffectPlate_EffectObject_Set);
    }

    /// <summary>
    /// アーツ名フレームが出現するときに流れるSE
    /// </summary>
    public static void EffectNameFrameSetPlaySe()
    {
    }

    /// <summary>
    /// アーツ名が出現するときに流れるSE
    /// </summary>
    public static void ArtsNameSetPlaySe()
    {
        SE_Manager.SePlay(SE_Manager.SE_NAME.EffectPlate_ArtsName_Set);
    }

    /// <summary>
    /// アーツデッキを切り替えるときに流れるSE
    /// </summary>
    public static void ArtsDeckChangePlaySe()
    {
        SE_Manager.SePlay(SE_Manager.SE_NAME.EffectPlate_Switching);
    }

    #endregion
}
