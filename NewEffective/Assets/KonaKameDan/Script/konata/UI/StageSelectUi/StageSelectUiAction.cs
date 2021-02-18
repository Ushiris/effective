using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUiAction : Image2DAnimation
{
    [SerializeField] float startActionSpeed = 100;
    [SerializeField] float waitingActionSpeed = 3;
    [SerializeField] float waitingActionH = 15;

    [Header("ステージイメージ")]
    [SerializeField] Image stageImage;
    [SerializeField] float stageImageActiveSpeed = 50;
    float stageImageGage = kStageImageStartGage;
    bool isStageImageAction = true;
    Material material;

    bool isFast;

    static readonly float kFixH = 250;
    static readonly float kStageImageStartGage = 14f;
    static readonly string kStageImageGagePass = "Vector1_AD5DC330";
    static readonly string kStageImagePass = "Texture2D_97707CC2";

    private void Awake()
    {
        //このMaterialを適応している物全てに影響を与えないための対策
        material = stageImage.material = new Material(stageImage.material);
        material.SetFloat(kStageImageGagePass, stageImageGage);
    }

    private void OnEnable()
    {
        if (isFast) StartUp();
        else ActionReset();
        isStageImageAction = true;
        stageImageGage = kStageImageStartGage;
        material.SetFloat(kStageImageGagePass, stageImageGage);
        AppearFromBelowUI_Acceleration(startActionSpeed, kFixH);
        OffScreen_Up(startActionSpeed, kFixH);
    }

    // Update is called once per frame
    void Update()
    {
        if (!StageSelectUiView.IsEndProcess)
        {
            if (!GetStartAction)
            {
                UpDownSinAction(waitingActionSpeed, waitingActionH);

                if (isStageImageAction)
                {
                    isStageImageAction = OnStageAction();
                }
                else
                {
                    //OnStageImageNoiseMove();
                }
            }
            else
            {
                StartActionUpdate();
            }
        }
        else
        {
            EndActionUpdate();
        }
    }

    //ステージイメージを出現させるためのShaderの操作
    bool OnStageAction()
    {
        stageImageGage -= Time.unscaledDeltaTime * stageImageActiveSpeed;
        stageImageGage = Mathf.Clamp(stageImageGage, 0, kStageImageStartGage);
        material.SetFloat(kStageImageGagePass, stageImageGage);
        if (stageImageGage == 0) return false;
        else return true;
    }

    //Noiseの演出
    void OnStageImageNoiseMove()
    {
        material.SetFloat("Vector1_39D1247F", Time.unscaledDeltaTime * 20f);
    }

    /// <summary>
    /// ステージイメージ(画像)を変更する
    /// </summary>
    /// <param name="texture"></param>
    public void OnStageImageChange(Texture texture)
    {
        material.SetTexture(kStageImagePass, texture);
    }

    /// <summary>
    /// UIの出現演出時は真
    /// </summary>
    /// <returns></returns>
    public bool isStartProcess() { return isStageImageAction; }

    /// <summary>
    /// UIが消える演出時は真
    /// </summary>
    /// <returns></returns>
    public bool isEndProcess() { return GetEndAction; }
}
