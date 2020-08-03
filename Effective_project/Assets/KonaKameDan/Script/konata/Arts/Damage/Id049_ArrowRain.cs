﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id049_ArrowRain : MonoBehaviour
{
    [Header("生成位置目印用")]
    [SerializeField] GameObject pointPosObj;
    GameObject instantPointPos;

    [Header("魔法陣")]
    [SerializeField] GameObject magicCircleObj;
    [SerializeField] float magicCircleSizUpSpeed = 0.1f;
    [SerializeField] Vector3 magicCircleMaxSiz = new Vector3(1, 1, 1);
    GameObject magicCircle;
    ObjSizChange isMagicCircleSiz;

    [Header("アローレイン")]
    [SerializeField] GameObject arrowRainParticle;

    [Header("アローレインの生成高さ")]
    [SerializeField] float instantHigh = 20f;

    [Header("ダメージ")]
    [SerializeField] float defaultDamage = 0.8f;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.01f;

    [Header("飛翔のスタック数に応じてたされる数")]
    [SerializeField] float plusTime = 0.2f;

    bool isStart;
    bool isArrowRain;

    ArtsStatus artsStatus;

    int shotCount;
    int flyCount;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        transform.parent = null;

        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);
        flyCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Fly);

        //ダメージの計算
        damage = Arts_Process.GetDamage(defaultDamage, plusDamage, shotCount);

        //生成位置決める用の目印オブジェクト生成
        instantPointPos = Instantiate(pointPosObj,Vector3.zero,new Quaternion());
        instantPointPos.transform.localScale = new Vector3(5, 0.1f, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (instantPointPos != null)
        {
            if (Input.GetMouseButton(0))
            {
                //生成するポジションを決めるときの目印を動かす
                instantPointPos.transform.position =
                    Arts_Process.GetMouseRayHitPos(instantPointPos.transform.position, "Ground", "Map");

                isStart = true;
            }
            else if (Input.GetMouseButtonUp(0) && isStart)
            {
                //生成するポジションを決める
                Vector3 instantPos = instantPointPos.transform.position;
                instantPos.y += instantHigh;
                Destroy(instantPointPos);

                //魔法陣の生成
                magicCircle = Instantiate(magicCircleObj, instantPos, new Quaternion());
                magicCircle.transform.parent = transform;
                isMagicCircleSiz = Arts_Process.SetAddObjSizChange(
                    magicCircle,Vector3.zero, magicCircleMaxSiz,
                    magicCircleSizUpSpeed,
                    ObjSizChange.SizChangeMode.ScaleUp);
            }
        }
        else
        {
            if (isMagicCircleSiz.GetSizFlag)
            {
                //アローレインパーティクルの生成
                if (!isArrowRain)
                {
                    //持続時間変更
                    var p = arrowRainParticle.GetComponent<ParticleSystem>();
                    var main = p.main;
                    main.duration += plusTime * (float)flyCount;

                    GameObject arrowRainObj = Instantiate(arrowRainParticle, magicCircle.transform);
                    isArrowRain = true;

                    //ダメージ
                    var hit = Arts_Process.SetParticleDamageProcess(arrowRainObj);
                    //ダメージ処理
                    Arts_Process.Damage(hit, artsStatus, damage, true);
                }
                if (magicCircle.transform.childCount == 0)
                {
                    //オブジェクトを消す
                    if (isMagicCircleSiz.SetSizChangeMode == ObjSizChange.SizChangeMode.ScaleDown)
                    {
                        Destroy(gameObject);
                    }

                    //魔法陣を小さくするモードに変更
                    isMagicCircleSiz.SetSizChangeMode = ObjSizChange.SizChangeMode.ScaleDown;
                    isMagicCircleSiz.GetSizFlag = false;
                }
            }
        }

    }
}
