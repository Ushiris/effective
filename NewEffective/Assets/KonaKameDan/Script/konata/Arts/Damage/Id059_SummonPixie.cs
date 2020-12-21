using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id059_SummonPixie : MonoBehaviour
{
    [SerializeField] GameObject fairyParticleObj;
    GameObject fairyParticle;

    [SerializeField] GameObject hollyShotParticleObj;
    [SerializeField] Vector3 instantPos = new Vector3(2, 2, 0);
    [SerializeField] float lostTime = 10f;
    [SerializeField] float speed = 5f;
    [SerializeField] float distance = 3f;

    [Header("ダメージ")]
    [SerializeField] float defaultDamage = 0.4f;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.01f;

    [Header("追尾のスタック数に応じてたされる数")]
    [SerializeField] float plusTime = 0.5f;

    [Header("飛翔のスタック数に応じてたされる数")]
    [SerializeField] float defaultAttackFrequency = 1f;
    [SerializeField] float attackFrequency = 0.01f;

    StopWatch timer;
    GameObject enemy;
    SE_Manager.Se3d se;

    int shotCount;
    int homingCount;
    int flyCount;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        var artsStatus = GetComponent<ArtsStatus>();
        transform.parent = null;

        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);
        homingCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Homing);
        flyCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Fly);

        //ダメージの計算
        damage = Arts_Process.GetDamage(defaultDamage, plusDamage, shotCount);

        //持続時間変更
        lostTime += plusTime * (float)homingCount;

        //ターゲットが存在していない場合消す
        enemy = Arts_Process.GetNearTarget(artsStatus);
        if (enemy == null) Destroy(gameObject);

        //妖精とビームパーティクルの生成
        fairyParticle = Instantiate(fairyParticleObj, transform);
        GameObject hollyShotParticle = Instantiate(hollyShotParticleObj, fairyParticle.transform);
        fairyParticle.transform.localPosition = instantPos;

        //攻撃頻度変更
        var p = hollyShotParticle.GetComponent<ParticleSystem>();
        var emi = p.emission;
        emi.rateOverTime = defaultAttackFrequency + (attackFrequency * (float)flyCount);

        //ダメージ
        var hit = Arts_Process.SetParticleDamageProcess(hollyShotParticle);
        //ダメージ処理
        Arts_Process.Damage(hit, artsStatus, damage, true);

        //〇〇秒後オブジェクトを破壊する
        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = lostTime;
        timer.LapEvent = () =>
        {
            //SE
            SE_Manager.SePlay(SE_Manager.SE_NAME.Id59_Funnel_third); 
            Destroy(gameObject);
        };

        //SE
        se = SE_Manager.Se3dPlay(SE_Manager.SE_NAME.Id059_SummonPixie_first);
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

            SE_Manager.Se3dMove(fairyParticle.transform.position, se);
        }
        else
        {
            //SE
            SE_Manager.SePlay(SE_Manager.SE_NAME.Id59_Funnel_third);

            Destroy(gameObject);
        }
    }
}
