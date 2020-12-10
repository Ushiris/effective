using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id57_Bomber : MonoBehaviour
{
    [SerializeField] GameObject bomberObj;
    [SerializeField] GameObject explosionParticleObj;
    [SerializeField] float rollSpeed = 100f;
    [SerializeField] float defaultDamage = 0.7f;

    [Header("拡散のスタック数に応じてたされる数")]
    [SerializeField] float plusCount = 0.02f;

    [Header("爆発のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.07f;

    List<GameObject> bomber = new List<GameObject>();

    int maxCount = 1;

    ArtsStatus artsStatus;
    ParticleHitPlayExplosion playExplosion;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //修正
        transform.parent = null;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        var explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);
        var spreadCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Spread);

        //ダメージの計算
        var damage = defaultDamage + (plusDamage * (float)explosionCount);

        var f = (float)spreadCount * plusCount;
        Debug.Log("f " + f + " s " + spreadCount);
        maxCount += (int)f;

        //円状の座標取得
        var pos = Arts_Process.GetCirclePutPos(maxCount, 1, 360);

        //生成
        for (int i = 0; i < maxCount; i++)
        {
            bomber.Add(Instantiate(bomberObj, transform));
            bomber[i].transform.localPosition = pos[i];

            //ダメージ
            var boomDamage = Arts_Process.SetParticleDamageProcess(bomber[i]);

            //ダメージ処理
            Arts_Process.Damage(boomDamage, artsStatus, damage, true);


            //触れたものを爆発させる
            playExplosion =
                Arts_Process.SetParticleHitPlay(bomber[i], explosionParticleObj, transform, artsStatus);

            var obj = bomber[i];
            playExplosion.OnExplosion += () => 
            {
                Destroy(obj);
            };
        }

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id257_Haiyoru_first);
    }

    // Update is called once per frame
    void Update()
    {
        //爆弾を破壊
        if (playExplosion.isTrigger == true)
        {
        }
        else
        {
            //回す
            transform.position = artsStatus.myObj.transform.position;
            Arts_Process.ObjRoll(gameObject, rollSpeed);
        }

        //消す
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
