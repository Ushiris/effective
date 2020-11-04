using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id019_Kamaitati : MonoBehaviour
{
    [SerializeField] GameObject boomerangObj;
    [SerializeField] float force = 30f;

    GameObject target;

    new ParticleSystem particleSystem;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //ブーメラン生成
        var boomerang = Instantiate(boomerangObj, transform);
        particleSystem = boomerang.GetComponent<ParticleSystem>();
    }
 
    private void Update()
    {
        //戻ってくる
        target = artsStatus.myObj;
        if (particleSystem != null && boomerangObj != null)
        {
            Arts_Process.HomingParticle(particleSystem, target, force);
        }

        //オブジェクトを消す
        if (transform.childCount == 0) Destroy(gameObject);
    }
}