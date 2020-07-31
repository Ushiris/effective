using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id024_Diffusion : MonoBehaviour
{
    int count = 4;

    [SerializeField] GameObject beamParticleObj;
    [SerializeField] float force = 100f;
    [SerializeField] float radius = 2f;
    [SerializeField] float lostTime = 3f;

    [Header("ダメージ")]
    [SerializeField] float defaultDamage = 0.8f;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.01f;


    [Header("拡散")]
    [SerializeField] int defaultBullet = 4;

    [Header("拡散のスタック数に応じてたされる数")]
    [SerializeField] int addBullet = 2;

    StopWatch timer;
    ArtsStatus artsStatus;
    ParticleHit[] hit;

    int shotCount;
    int barrierCount;
    int spreadCount;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        transform.parent = null;

        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);
        spreadCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Spread);

        //拡散数
        count = defaultBullet + (spreadCount * addBullet);
        hit = new ParticleHit[count];

        //ダメージの計算
        damage = Arts_Process.GetDamage(defaultDamage, plusDamage, shotCount);

        //円状の座標取得
        var pos = Arts_Process.GetCirclePutPos(count, radius, 360);

        GameObject[] beam = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            //円状に配置する
            beam[i] = Instantiate(beamParticleObj, transform);
            beam[i].transform.localPosition = pos[i];
            beam[i].transform.LookAt(transform);

            //移動
            Arts_Process.RbMomentMove(beam[i], force);


            //ダメージ
            hit[i] = Arts_Process.SetParticleDamageProcess(beam[i]);
            //ダメージ処理
            Arts_Process.Damage(hit[i], artsStatus, damage, true);
        }

        //オブジェクトの破壊
        var timer = Arts_Process.TimeAction(gameObject, lostTime);
        timer.LapEvent = () => { Destroy(gameObject); };
    }

    // Update is called once per frame
    void Update()
    {
    }
}
