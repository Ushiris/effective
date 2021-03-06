﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id015_Valkyrie : MonoBehaviour
{
    [SerializeField] GameObject fairyParticleObj;
    GameObject fairyParticle;

    [SerializeField] GameObject flySlashParticleObj;
    [SerializeField] Vector3 instantPos = new Vector3(2, 2, 0);
    [SerializeField] float lostTime = 10f;
    [SerializeField] float speed = 5f;
    [SerializeField] float distance = 3f;

    GameObject enemy;

    StopWatch timer;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        transform.parent = null;

        //ターゲットが存在していない場合消す
        enemy = Arts_Process.GetNearTarget(artsStatus);
        if (enemy == null) Destroy(gameObject);

        //妖精と斬撃の生成
        fairyParticle = Instantiate(fairyParticleObj, transform);
        GameObject flySlashParticle = Instantiate(flySlashParticleObj, fairyParticle.transform);
        var ps = flySlashParticle.GetComponent<ParticleSystem>();
        var main= ps.main;
        main.loop = true;
        fairyParticle.transform.localPosition = instantPos;

        //〇〇秒後オブジェクトを破壊する
        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = lostTime;
        timer.LapEvent = () => { Destroy(gameObject); };
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            //妖精をエネミーの近くまで移動
            Vector3 pos = fairyParticle.transform.position;
            float dis = Vector3.Distance(pos, enemy.transform.position);
            if (distance < dis)
            {
                pos = Vector3.Lerp(pos, enemy.transform.position, Time.deltaTime * speed);
                fairyParticle.transform.position = pos;
            }

            //敵の方向を見る
            fairyParticle.transform.rotation =
                Arts_Process.GetLookRotation(fairyParticle.transform, enemy.transform);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
