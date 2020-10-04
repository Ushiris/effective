using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id079_Amaterasu : MonoBehaviour
{
    [SerializeField] GameObject satelliteCannonParticleObj;
    [SerializeField] GameObject satelliteCannonStartParticleObj;
    [SerializeField] GameObject satelliteCannonParticleHitObj;
    [SerializeField] Vector3 instantPos = new Vector3(0, 1, 5);
    [SerializeField] float sizUpSpeed = 3;
    [SerializeField] float defaultDamage = 0.1f;
    [SerializeField] float duration = 5f;

    GameObject satelliteCannonParticle;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamageShot = 0.01f;

    [Header("爆発のスタック数に応じてたされる数")]
    [SerializeField] float plusDamageExplosion = 0.01f;

    [Header("飛翔のスタック数に応じてたされる数")]
    [SerializeField] float plusTime = 0.2f;

    int shotCount;
    int explosionCount;
    int flyCount;
    float damage;

    int frame = 0;
    bool isSatelliteCannonInstant;
    Vector3? hitParticlePos;
    GameObject satelliteCannonStart;

    HitCollision hitCollision;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);
        explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);
        flyCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Fly);

        //持続時間の計算
        duration += flyCount * plusTime;

        //位置の初期設定
        transform.localPosition = instantPos;
        Arts_Process.RollReset(gameObject);
        var pos = transform.position;
        pos.y = 0;
        transform.position = pos;

        //初手の演出生成
        satelliteCannonStart = Instantiate(satelliteCannonStartParticleObj, transform);
    }

    // Update is called once per frame
    void Update()
    {
        //初手の演出が終わったら実行
        if (satelliteCannonStart == null && !isSatelliteCannonInstant)
        {
            isSatelliteCannonInstant = true;

            //サテライトキャノンの生成
            satelliteCannonParticle = Instantiate(satelliteCannonParticleObj, transform);
            satelliteCannonParticle.transform.localPosition = new Vector3(0, 30, 0);
            satelliteCannonParticle.transform.localScale = new Vector3(10, 0, 10);
            hitCollision = Arts_Process.SetHitCollision(satelliteCannonParticle);
            hitCollision.tags.Add("Ground");

            var satelliteBeamMaterial = satelliteCannonParticle.GetComponent<SatelliteBeamMaterial>();
            satelliteBeamMaterial.coolTime = duration;

            Damage(satelliteCannonParticle);
        }

        if (hitCollision != null)
        {
            //地面に当たるまでオブジェクトを伸ばす
            if (!hitCollision.GetOnTrigger)
            {
                var siz = satelliteCannonParticle.transform.localScale;
                siz.y += Time.deltaTime * sizUpSpeed;
                satelliteCannonParticle.transform.localScale = siz;
            }
            else
            {
                //20フレームごとにHitParticleを生成
                if (frame % 20 == 0)
                {
                    if (hitParticlePos == null) hitParticlePos = HitParticlePos();
                    else
                    {
                        var obj = Instantiate(satelliteCannonParticleHitObj, transform);
                        obj.transform.position = (Vector3)hitParticlePos;
                    }
                }
                frame++;
            }
        }

        //削除
        if (transform.childCount == 0) Destroy(gameObject);
    }

    Vector3 HitParticlePos()
    {
        var pos = satelliteCannonParticle.transform.position;
        var t = NewMapTerrainData.GetTerrain;
        pos.y = t.terrainData.GetHeight((int)pos.x, (int)pos.z);
        return pos;
    }

    void Damage(GameObject obj)
    {
        //ダメージ
        var satelliteCannonDamage = Arts_Process.SetParticleZoneDamageProcess(obj);

        //ダメージの計算
        var d1 = defaultDamage + (plusDamageShot * (float)shotCount);
        var d2 = defaultDamage + (plusDamageExplosion * (float)explosionCount);
        damage = d1 + d2;

        //ダメージ処理
        Arts_Process.ZoneDamage(satelliteCannonDamage, artsStatus, damage, true);
    }
}
