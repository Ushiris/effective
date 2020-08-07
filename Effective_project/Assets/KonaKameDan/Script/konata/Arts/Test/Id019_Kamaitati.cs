using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id019_Kamaitati : MonoBehaviour
{
    [SerializeField] GameObject boomerangObj;
    [SerializeField] float force = 30f;

    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        transform.parent = null;

        //ブーメラン生成
        var boomerang = Instantiate(boomerangObj, transform);

        //スプリングジョイントの登録
        var sjBoomerang = boomerang.GetComponent<SpringJoint>();
        sjBoomerang.connectedBody = artsStatus.myObj.GetComponent<Rigidbody>();

        //移動
        Arts_Process.RbMomentMove(boomerang, force);

        //仮
        Destroy(gameObject, 10f);
    }
}
