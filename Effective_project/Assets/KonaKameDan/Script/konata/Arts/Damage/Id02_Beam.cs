using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id02_Beam : MonoBehaviour
{
    [SerializeField] GameObject beamParticleObj;
    [SerializeField] float force = 100f;
    [SerializeField] float lostTime = 3f;

    [Header("ダメージ")]
    [SerializeField] float defaultDamage = 0.8f;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.08f;

    StopWatch timer;
    ArtsStatus artsStatus;
    ParticleHit hit;

    int shotCount;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        transform.parent = null;

        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);

        //ダメージの計算
        damage = Arts_Process.GetDamage(defaultDamage, plusDamage, shotCount);

        //生成
        GameObject beam = Instantiate(beamParticleObj, transform);
        Arts_Process.SetParticleDamageProcess(beam);
        Arts_Process.RbMomentMove(beam, force);

        //ダメージ
        hit = Arts_Process.SetParticleDamageProcess(beam);
        //ダメージ処理
        Arts_Process.Damage(hit, artsStatus, damage, true);

        timer = Arts_Process.TimeAction(gameObject, lostTime);
        timer.LapEvent = () => { Destroy(gameObject); };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
