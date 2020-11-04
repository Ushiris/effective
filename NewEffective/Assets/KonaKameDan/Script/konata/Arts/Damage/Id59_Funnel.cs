using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id59_Funnel : MonoBehaviour
{
    [SerializeField] GameObject fairyParticleObj;
    GameObject fairyParticle;

    [SerializeField] GameObject DestParticleObj;

    [SerializeField] GameObject hollyShotParticleObj;

    [SerializeField] Vector3 instantPos = new Vector3(2, 2, 0);
    [SerializeField] float lostTime = 10f;

    [Header("ダメージ")]
    [SerializeField] float defaultDamage = 0.3f;

    [Header("飛翔のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.01f;

    [Header("追尾のスタック数に応じてたされる数")]
    [SerializeField] float plusTime = 0.2f;

    StopWatch timer;
    ArtsStatus artsStatus;
    ParticleHit hit;

    int flyCount;
    int homingCount;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //オブジェクトが登録されている場合このオブジェクトを消す
        var objs = ArtsActiveObj.Id59_Funnel;
        Arts_Process.OldArtsDestroy(objs, artsStatus.myObj);

        //エフェクトの所持数を代入
        flyCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Fly);
        homingCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Homing);

        //消失時間の変更
        lostTime += plusTime * (float)homingCount;

        //ダメージの計算
        damage = Arts_Process.GetDamage(defaultDamage, plusDamage, flyCount);

        //妖精とビームパーティクルの生成
        fairyParticle = Instantiate(fairyParticleObj, transform);
        GameObject hollyShotParticle = Instantiate(hollyShotParticleObj, fairyParticle.transform);
        fairyParticle.transform.localPosition = instantPos;

        //ダメージ
        hit = Arts_Process.SetParticleDamageProcess(hollyShotParticle);
        //ダメージ処理
        Arts_Process.Damage(hit, artsStatus, damage, true);

        //〇〇秒後オブジェクトを破壊する
        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = lostTime;
        timer.LapEvent = () => { Lost(); };
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

    void Lost()
    {
        ArtsActiveObj.Id59_Funnel.Remove(artsStatus.myObj);
        Instantiate(DestParticleObj, transform.position, new Quaternion());
        Destroy(gameObject);
    }
}
