using System.Collections;
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

    SE_Manager.Se3d se;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();
        Arts_Process.RollReset(gameObject);

        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);
        flyCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Fly);

        //ダメージの計算
        damage = Arts_Process.GetDamage(defaultDamage, plusDamage, shotCount);

        //魔法陣の生成
        Vector3 instantPos = new Vector3(0, 10, 12);
        magicCircle = Instantiate(magicCircleObj,transform);
        magicCircle.transform.localPosition = instantPos;
        isMagicCircleSiz = Arts_Process.SetAddObjSizChange(
            magicCircle, Vector3.zero, magicCircleMaxSiz,
            magicCircleSizUpSpeed,
            ObjSizChange.SizChangeMode.ScaleUp);

        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id049_ArrowRain_first, transform.position, artsStatus);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMagicCircleSiz.GetSizFlag)
        {
            //アローレインパーティクルの生成
            if (!isArrowRain)
            {
                //持続時間変更
                GameObject arrowRainObj = Instantiate(arrowRainParticle, magicCircle.transform);
                var p = arrowRainObj.GetComponent<ParticleSystem>();
                p.Stop();
                var main = p.main;
                main.duration += plusTime * (float)flyCount;
                p.Play();
                isArrowRain = true;

                //ダメージ
                var hit = Arts_Process.SetParticleDamageProcess(arrowRainObj);
                //ダメージ処理
                Arts_Process.Damage(hit, artsStatus, damage, true);

                //SE
                se=Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id049_ArrowRain_second, transform.position, artsStatus);
            }
            if (magicCircle.transform.childCount == 0)
            {
                //オブジェクトを消す
                if (isMagicCircleSiz.SetSizChangeMode == ObjSizChange.SizChangeMode.ScaleDown && se.se.volume <= 0.0f)
                {
                    Destroy(gameObject);
                }

                //魔法陣を小さくするモードに変更
                isMagicCircleSiz.SetSizChangeMode = ObjSizChange.SizChangeMode.ScaleDown;
                isMagicCircleSiz.GetSizFlag = false;

                //SEをフェード
                SE_Manager.SetFadeOut(this, se.se, 0.5f);
            }
        }
    }
}
