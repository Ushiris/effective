using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id079_Amaterasu : MonoBehaviour
{
    [SerializeField] GameObject satelliteCannonParticleObj;
    [SerializeField] GameObject satelliteCannonStartParticleObj;
    [SerializeField] ParticleSystem satelliteCannonParticleHit;
    [SerializeField] Vector3 instantPos = new Vector3(0, 1, 5);
    [SerializeField] float sizUpSpeed = 3;
    [SerializeField] float defaultDamage = 0.1f;
    [SerializeField] float duration = 5f;

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

    bool isSatelliteCannonInstant;
    bool isPlayHitParticle;

    SatelliteBeamMaterial satelliteBeamMaterial;
    ParticleHitZoneDamage satelliteCannonDamage;
    HitCollision hitCollision;
    ArtsStatus artsStatus;

    private void Awake()
    {
        //宣言＆アタッチ
        artsStatus = GetComponent<ArtsStatus>();
        satelliteCannonDamage = Arts_Process.SetParticleZoneDamageProcess(satelliteCannonParticleObj);
        Arts_Process.ZoneDamage(satelliteCannonDamage, artsStatus, damage, false);
        hitCollision = Arts_Process.SetHitCollision(satelliteCannonParticleObj);
        satelliteBeamMaterial = satelliteCannonParticleObj.GetComponent<SatelliteBeamMaterial>();

        satelliteCannonParticleObj.transform.localScale = new Vector3(10, 0, 10);
    }

    // Start is called before the first frame update
    void Start()
    {

        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);
        explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);
        flyCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Fly);

        //持続時間の計算
        duration += flyCount * plusTime;

        //位置の初期設定
        transform.localPosition = instantPos;
        Arts_Process.RollReset(gameObject);
        //var pos = transform.position;
        //pos.y = 0;
        //transform.position = pos;

        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id079_Amaterasu_first, transform.position, artsStatus);
    }

    // Update is called once per frame
    void Update()
    {
        //初手の演出が終わったら実行
        if (!satelliteCannonStartParticleObj.activeSelf && !isSatelliteCannonInstant)
        {
            isSatelliteCannonInstant = true;

            //ビームが動く準備
            satelliteBeamMaterial.coolTime = duration;
            satelliteBeamMaterial.isTimeStart = true;

            Damage();
        }

        if (isSatelliteCannonInstant)
        {
            //地面に当たるまでオブジェクトを伸ばす
            if (!hitCollision.GetOnTrigger)
            {
                var siz = satelliteCannonParticleObj.transform.localScale;
                siz.y += Time.deltaTime * sizUpSpeed;
                satelliteCannonParticleObj.transform.localScale = siz;
            }
            else
            {
                if (!isPlayHitParticle)
                {
                    //地面にヒットしたらParticleをPlayする
                    isPlayHitParticle = true;
                    satelliteCannonParticleHit.gameObject.transform.position = HitParticlePos();
                    satelliteCannonParticleHit.Play();
                }
                else if (!satelliteBeamMaterial.isTimeStart)
                {
                    //アーツの全ての処理を終えた場合、プールに戻す
                    //StartUpParticle.SetArts("Id079_Amaterasu", artsStatus);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnDisable()
    {
        //初期化
        isPlayHitParticle = false;
        isSatelliteCannonInstant = false;
        satelliteCannonStartParticleObj.SetActive(true);
    }

    //テラインの高さを取得して地面の高さに合わせる
    Vector3 HitParticlePos()
    {
        var pos = satelliteCannonParticleObj.transform.position;
        var t = NewMapTerrainData.GetTerrain;
        pos.y = t.terrainData.GetHeight((int)pos.x, (int)pos.z);
        return pos;
    }

    void Damage()
    {
        //ダメージの計算
        var d1 = defaultDamage + (plusDamageShot * (float)shotCount);
        var d2 = defaultDamage + (plusDamageExplosion * (float)explosionCount);
        damage = d1 + d2;

        //ダメージ処理
        Arts_Process.ZoneDamage(satelliteCannonDamage, artsStatus, damage, true);
    }
}
