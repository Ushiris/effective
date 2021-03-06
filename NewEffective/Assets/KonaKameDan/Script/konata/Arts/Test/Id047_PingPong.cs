﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id047_PingPong : MonoBehaviour
{
    [SerializeField] GameObject explosionParticleObj;
    [SerializeField] GameObject bulletObj;
    [SerializeField] float force = 30f;
    [SerializeField] float lostTime = 3.5f;
    [SerializeField] Vector3 v0 = new Vector3(5, 5, 10);
    [SerializeField] float defaultDamage = 1f;
    [SerializeField] int defaultBulletCount = 3;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.05f;

    [Header("拡散のスタック数に応じてたされる数")]
    [SerializeField] float addBullet = 0.05f;

    //エフェクトの所持数用
    int shotCount = 0;
    int spreadCount = 0;
    float damage;

    Vector3 pos;
    List<GameObject> bullet = new List<GameObject>();

    ParticleHit bulletDamage;
    StopWatch timer;
    ArtsStatus artsStatus;
    ParticleHitPlayExplosion particleHitPlay;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //親子解除
        transform.parent = null;

        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);
        spreadCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Spread);

        //弾数を増やす計算
        float bulletCount = addBullet * (float)spreadCount + (float)defaultBulletCount;
        bulletCount = Mathf.Floor(bulletCount);
        float bulletDir = (bulletCount / 2 + 0.5f) - bulletCount;

        //ダメージの計算
        damage = defaultDamage + (plusDamage * (float)shotCount);

        //弾の生成
        for (int i = 0; i < (int)bulletCount; i++)
        {
            bullet.Add(Instantiate(bulletObj, transform));
            var rb = bullet[i].GetComponent<Rigidbody>();
            v0.x = bulletDir + i;
            bullet[i].transform.localPosition = new Vector3(v0.x * 0.2f, 0, 0);
            rb.AddRelativeFor​​ce(v0, ForceMode.VelocityChange);

            //爆発するエフェクトのセット
            particleHitPlay =
                Arts_Process.SetParticleHitPlay(bullet[i], explosionParticleObj, transform, artsStatus);

            //ダメージ
            bulletDamage = Arts_Process.SetParticleDamageProcess(bullet[i]);

            //ダメージ処理
            Arts_Process.Damage(bulletDamage, artsStatus, damage, true);

            particleHitPlay.OnExplosion += () => 
            {
                Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id047_PingPong_third, bullet[i].transform.position, artsStatus);
            };
        }

        var timer = Arts_Process.TimeAction(gameObject, lostTime);
        timer.LapEvent = () => { Lost(bullet); };

        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id047_PingPong_first, transform.position, artsStatus);
    }

    private void Update()
    {
        if (particleHitPlay.isTrigger) Lost(bullet);
    }

    //一定時間たったら爆発する
    void Lost(List<GameObject> obj)
    {
        if (obj != null)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                pos = obj[i].transform.position;
                var explosion = Instantiate(explosionParticleObj, pos, Quaternion.identity);

                //SE
                Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id047_PingPong_third, pos, artsStatus);

                Destroy(obj[i]);
                Destroy(gameObject, 2);
            }
        }
    }
}
