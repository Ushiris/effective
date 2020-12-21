using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id457_BigBang : MonoBehaviour
{
    [SerializeField] Vector3 pos = new Vector3(0, 0, 5f);
    [SerializeField] float vacuumPower = 50;
    [SerializeField] float explosionTiming = 5f;
    [SerializeField] float defaultDamage = 4f;

    [SerializeField] SphereCollider collider;
    [SerializeField] GameObject explosionObj;
    [SerializeField] GameObject vacuumParticle;

    [Header("拡散のスタック数に応じてたされる数")]
    [SerializeField] float plusSiz = 0.02f;

    [Header("爆発のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.1f;

    float damage;

    StopWatch timer;
    SE_Manager.Se3d se1;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        transform.localPosition = pos;
        Arts_Process.RollReset(gameObject);
        Arts_Process.GroundPosMatch(gameObject);

        //エフェクト所持数
        var spreadCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Spread);
        var explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);

        collider.radius += (float)spreadCount * plusSiz;

        //ダメージの計算
        damage = defaultDamage + (plusDamage * (float)explosionCount);

        //一定時間ごとに隕石を生成
        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = explosionTiming;
        timer.LapEvent = () => { Explosion(); };

        //SE
        se1 = SE_Manager.Se3dPlay(SE_Manager.SE_NAME.Id457_BigBang_second);
        SE_Manager.Se3dMove(transform.position, se1);
    }

    void Explosion()
    {
        vacuumParticle.SetActive(false);
        collider.enabled = false;
        explosionObj.SetActive(true);
        Destroy(gameObject, 1.5f);

        //SE
        var se2 = SE_Manager.Se3dPlay(SE_Manager.SE_NAME.Id047_PingPong_third);
        SE_Manager.Se3dMove(transform.position, se2);
        SE_Manager.ForcedPlayStop(se1.se);

        //ダメージの処理
        var damageProcess = Arts_Process.SetParticleDamageProcess(explosionObj);
        Arts_Process.Damage(damageProcess, artsStatus, damage, true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Arts_Process.Vacuum(other.gameObject, transform.position, vacuumPower);
        }
    }
}
