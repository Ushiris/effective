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
   
    bool isStart;
    bool isArrowRain;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;

        //生成位置決める用の目印オブジェクト生成
        instantPointPos = Instantiate(pointPosObj,Vector3.zero,new Quaternion());
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
                    GameObject arrowRainObj = Instantiate(arrowRainParticle, magicCircle.transform);
                    isArrowRain = true;
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
