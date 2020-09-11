﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id079_Amaterasu : MonoBehaviour
{
    [SerializeField] GameObject satelliteCannonParticleObj;
    [SerializeField] GameObject satelliteCannonStartParticleObj;
    [SerializeField] Vector3 instantPos = new Vector3(0, 0, 5);

    bool isSatelliteCannonInstant;
    GameObject satelliteCannonStart;

    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {

        //artsStatus = GetComponent<ArtsStatus>();

        //位置の初期設定
        transform.localPosition = instantPos;
        Arts_Process.RollReset(gameObject);

        //初手の演出生成
        satelliteCannonStart = Instantiate(satelliteCannonStartParticleObj, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (satelliteCannonStart == null && !isSatelliteCannonInstant)
        {
            isSatelliteCannonInstant = true;

            //サテライトキャノンの生成
            var obj= Instantiate(satelliteCannonParticleObj, transform);       
            obj.transform.localPosition = new Vector3(0, 10, 0);
            InstantCollider(obj);
        }

        //オブジェクトを消す
        if (isSatelliteCannonInstant)
        {
            if (transform.childCount == 0) Destroy(gameObject);
        }
    }

    void InstantCollider(GameObject obj)
    {
        var collider = obj.AddComponent<CapsuleCollider>();
        collider.isTrigger = true;
        collider.center = new Vector3(0, 0, 2);
        collider.radius = 5;
        collider.height = 22;
        collider.direction = 2;
    }
}