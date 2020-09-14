﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id79_Grenade : MonoBehaviour
{
    [SerializeField] GameObject trajectoryObj;
    [SerializeField] GameObject grenadeObj;
    [SerializeField] GameObject bulletObj;
    [SerializeField] Vector3 v0 = new Vector3(0, 5, 7);
    [SerializeField] float trajectoryCount = 10;
    [SerializeField] float trajectorySpace = 0.1f;

    Vector3 pos;
    GameObject grenade;
    List<GameObject> miracles = new List<GameObject>();

    bool isStart = true;

    ArtsStatus artsStatus;
    ParticleHitPlayExplosion particleHitPlay;


    void Start()
    {
        for (int i = 0; i < trajectoryCount; i++)
        {
            miracles.Add(Instantiate(trajectoryObj, transform));
        }

        //軌跡を生成
        miracles = Arts_Process.Trajectory(miracles, trajectorySpace, v0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && isStart)
        {
            transform.parent = null;

            //Grenade生成
            grenade = Instantiate(bulletObj, transform);

            //grenadeを投げる
            Rigidbody jumpCubeRb = grenade.GetComponent<Rigidbody>();
            jumpCubeRb.AddRelativeFor​​ce(v0, ForceMode.VelocityChange);

            //爆発するエフェクトのセット
            particleHitPlay =
                Arts_Process.SetParticleHitPlay(grenade, grenadeObj, transform, artsStatus);

            isStart = false;
        }
    }
}