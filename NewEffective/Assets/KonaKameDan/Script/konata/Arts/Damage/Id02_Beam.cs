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
        var hit = Arts_Process.SetParticleZoneDamageProcess(beamCoreObj.gameObject);
        //ダメージ処理
        Arts_Process.ZoneDamage(hit, artsStatus, damage, true);

        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id024_Diffusion_first, transform.position, artsStatus);
    }

    // Update is called once per frame
    void Update()
    {
        if (beamControlScript.isGetEnd)
        {
            Destroy(gameObject, 1f);
        }
    }
}
