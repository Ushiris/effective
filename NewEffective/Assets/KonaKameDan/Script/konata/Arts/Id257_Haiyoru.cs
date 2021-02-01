using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id257_Haiyoru : MonoBehaviour
{
    [SerializeField] GameObject bomberObj;
    [SerializeField] GameObject explosionParticleObj;
    [SerializeField] float speed = 5f;
    [SerializeField] float rollSpeed = 5f;
    [SerializeField] float defaultDamage = 0.6f;

    [Header("防御のスタック数に応じてたされる数")]
    [SerializeField] float plusCount = 0.02f;

    [Header("爆発のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.08f;

    int maxCount = 1;
    bool isEnemy;
    GameObject target;
    List<GameObject> bomb = new List<GameObject>();

    ArtsStatus artsStatus;
    ParticleHitPlayExplosion playExplosion;

    bool oneShotSE_second;
    bool oneShotSE_third;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //修正
        transform.parent = null;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        var barrierCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Barrier);
        var explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);

        var f = (float)barrierCount * plusCount;
        maxCount += (int)f;

        //ダメージの計算
        var damage = defaultDamage + (plusDamage * (float)explosionCount);

        //円状の座標取得
        var radius = 1f;
        radius *= artsStatus.modelSiz;
        var pos = Arts_Process.GetCirclePutPos(maxCount, radius, 360);

        //生成
        for (int i = 0; i < maxCount; i++)
        {
            bomb.Add(Instantiate(bomberObj, transform));
            bomb[i].transform.localPosition = pos[i];

            //ダメージ
            var boomDamage = Arts_Process.SetParticleDamageProcess(bomb[i]);

            //ダメージ処理
            Arts_Process.Damage(boomDamage, artsStatus, damage, true);


            //触れたものを爆発させる
            playExplosion =
                Arts_Process.SetParticleHitPlay(bomb[i], explosionParticleObj, transform, artsStatus);

            var obj = bomb[i];
            playExplosion.OnExplosion += () =>
            {
                bomb.Remove(obj);
                Destroy(obj);
            };
        }

        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id257_Haiyoru_first, transform.position, artsStatus);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnemy)
        {
            //目標を取得
            target = Arts_Process.GetNearTarget(artsStatus);
            if (target != null)
            {
                isEnemy = true;
            }
        }
        else if (target == null || !target.activeSelf)
        {
            Destroy(gameObject);
        }

        if (target == null)
        {
            //回す
            transform.position = Arts_Process.GetCharacterChasePos(artsStatus);
            Arts_Process.ObjRoll(gameObject, rollSpeed);

            oneShotSE_second = true;
            oneShotSE_third = true;
        }
        else if (bomb.Count != 0)
        {
            //敵に向かう
            for (int i = 0; i < bomb.Count; i++)
            {
                var pos = bomb[i].transform.position;
                pos = Vector3.MoveTowards(pos, target.transform.position, Time.deltaTime * speed);
                bomb[i].transform.position = pos;
            }

            if (oneShotSE_second)
            {
                //SE
                Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id257_Haiyoru_second, transform.position, artsStatus);
                oneShotSE_second = false;
            }
        }

        //爆弾を破壊
        if (playExplosion.isTrigger == true)
        {
            if (oneShotSE_third)
            {
                //SE
                Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id047_PingPong_third, bomb[0].transform.position, artsStatus);

                oneShotSE_third = false;
            }
        }

        //消す
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
