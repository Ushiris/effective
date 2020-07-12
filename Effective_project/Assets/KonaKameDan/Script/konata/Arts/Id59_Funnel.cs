using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id59_Funnel : MonoBehaviour
{
    [SerializeField] GameObject fairyParticleObj;
    GameObject fairyParticle;

    [SerializeField] GameObject hollyShotParticleObj;

    [SerializeField] Vector3 instantPos = new Vector3(2, 2, 0);
    [SerializeField] float lostTime = 10f;

    StopWatch timer;
    ArtsStatus artsStatus;

    static bool isExistence;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //すでに存在している場合消去
        if (isExistence && !MainGameManager.GetArtsReset) Destroy(gameObject);
        isExistence = true;

        //妖精とビームパーティクルの生成
        fairyParticle = Instantiate(fairyParticleObj, transform);
        GameObject hollyShotParticle = Instantiate(hollyShotParticleObj, fairyParticle.transform);
        fairyParticle.transform.localPosition = instantPos;

        //〇〇秒後オブジェクトを破壊する
        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = lostTime;
        timer.LapEvent = () => { TimeOver(); };
    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemy = Arts_Process.GetNearTarget(artsStatus);
        if (enemy != null)
        {
            //敵の方向を見る
            fairyParticle.transform.rotation =
                Arts_Process.GetLookRotation(fairyParticle.transform, enemy.transform);
        }
    }

    void TimeOver()
    {
        isExistence = false;
        Destroy(gameObject);
    }
}
