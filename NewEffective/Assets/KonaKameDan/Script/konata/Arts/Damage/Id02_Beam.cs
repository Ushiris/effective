using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id02_Beam : MonoBehaviour
{
    [SerializeField] GameObject beamParticleObj;
    [SerializeField] float lostStartTime = 5;

    [Header("ダメージ")]
    [SerializeField] float defaultDamage = 0.8f;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.08f;

    ArtsStatus artsStatus;
    ParticleHit hit;
    Beam beamControlScript;

    int shotCount;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);

        //ダメージの計算
        damage = Arts_Process.GetDamage(defaultDamage, plusDamage, shotCount);

        //生成
        var beam = Instantiate(beamParticleObj, transform);
        beamControlScript = beam.GetComponent<Beam>();
        beamControlScript.lostStartTime = lostStartTime;
        var beamCoreObj = beamControlScript.GetBeamObj.transform.GetChild(0);


        //ダメージ
        hit = Arts_Process.SetParticleDamageProcess(beamCoreObj.gameObject);
        //ダメージ処理
        Arts_Process.Damage(hit, artsStatus, damage, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (beamControlScript.isGetEnd)
        {
            Destroy(gameObject);
        }
    }
}
