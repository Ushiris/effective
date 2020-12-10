using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id027_AnnihilationRay : MonoBehaviour
{
    [SerializeField] GameObject[] explosionObjArr;
    [SerializeField] ParticleSystem annihilationRayParticleObj;
    [SerializeField] Transform pivot;

    [SerializeField] float speed = 5f;
    [SerializeField] float defaultDamage = 0.8f;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamageShot = 0.02f;

    [Header("爆発のスタック数に応じてたされる数")]
    [SerializeField] float plusDamageExplosion = 0.02f;

    //エフェクトの所持数用
    int shotCount = 0;
    int explosionCount = 0;

    float timer;
    Quaternion roll;
    bool isPlaySe;

    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        Destroy(gameObject, 5);
        roll= pivot.transform.localRotation;
        isPlaySe = true;

        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);
        explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);

        //ダメージの計算
        var fixDamage = (plusDamageShot * (float)shotCount) + (plusDamageExplosion * (float)explosionCount);
        var damage = defaultDamage + fixDamage;

        for (int i = 0; i < explosionObjArr.Length; i++)
        {
            //ダメージ
            var particleHit = Arts_Process.SetParticleDamageProcess(explosionObjArr[i]);
            Arts_Process.Damage(particleHit, artsStatus, damage, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 1f)
        {
            timer += Time.deltaTime;
            return;
        }
        if(isPlaySe)
        {
            // SE
            SE_Manager.SePlay(SE_Manager.SE_NAME.Id027_annihilationRay_first);
            isPlaySe = false;
        }
        //回転
        float step = speed * Time.deltaTime;
        pivot.transform.localRotation =
            Quaternion.RotateTowards(pivot.transform.localRotation, Quaternion.Euler(0, 100, 0), step);
    }
}
