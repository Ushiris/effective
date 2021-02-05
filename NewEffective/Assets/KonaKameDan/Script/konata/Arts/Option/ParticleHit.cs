using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHit : MonoBehaviour
{
    public float hitDamageDefault = 3f;
    public float plusFormStatus;
    public ArtsStatus artsStatus;
    public bool isMapLayer;
    public bool isParticleCollision = true;
    public bool isEnemyKnockBack = true;

    public static bool isAllDamageEnable = true;

    string hitObjTag;
    bool isTrigger;
    ParticleSystem p;

    List<string> layerNameList = new List<string>()
    {
        "Default", "PostProcessing", "Map"
    };

    private void Start()
    {
        ParticleSystem.CollisionModule c;

        if (!isParticleCollision) p = null;
        else
        {
            p = GetComponent<ParticleSystem>();
            if (p != null) c = p.collision;
        }

        switch (artsStatus.type)
        {
            case ArtsStatus.ParticleType.Player:
                if (p != null) c.collidesWith = Layer("Enemy");
                gameObject.layer = LayerMask.NameToLayer("PlayerArts");
                hitObjTag = "Enemy";

                //ダメージUIを出すやつ
                gameObject.AddComponent<DamageHit>();
                break;

            case ArtsStatus.ParticleType.Enemy:
                if (p != null) c.collidesWith = Layer("Player");
                gameObject.layer = LayerMask.NameToLayer("EnemyArts");
                hitObjTag = "Player";
                break;

            default: break;
        }     
    }

    //パーティクルが当たった時
    private void OnParticleCollision(GameObject obj)
    {
        if (!isParticleCollision) return;
        if (obj.tag == null) return;
        if (obj.CompareTag(hitObjTag))
        {
            Damage(obj);
            isTrigger = true;
        }
    }

    //オブジェクトが貫通した時
    private void OnTriggerEnter(Collider other)
    {
        if (!OnDestroyArtsZone(other.gameObject))
        {
            if (other.CompareTag(hitObjTag))
            {
                Damage(other.gameObject);
                isTrigger = true;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //当たり判定のあるオブジェクト
    private void OnCollisionEnter(Collision collision)
    {
        if (!OnDestroyArtsZone(collision.gameObject))
        {
            if (collision.gameObject.CompareTag(hitObjTag))
            {
                Damage(collision.gameObject);
                isTrigger = true;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //ダメージの処理
    void Damage(GameObject obj)
    {
        if (!isAllDamageEnable) return;

        float damage = hitDamageDefault * plusFormStatus;
        int damageCast = Mathf.CeilToInt(damage);

        var life = obj.GetComponent<Life>();
        if (life != null) life.Damage(damageCast);
        DebugLogger.Log("ArtsName:" + artsStatus.gameObject.name + "name: " + artsStatus.myObj.name + " damage: " + damage + " damageCast: " + damageCast + " hitDamageDefault: " + hitDamageDefault);

        //ノックバック判定
        if (obj.CompareTag("Enemy") && isEnemyKnockBack)
        {
            var enemy = obj.GetComponent<Enemy>();
            if (enemy != null) enemy.KnockBack(100, 0.5f);
        }

        //UI&Point
        if (obj.CompareTag("Enemy"))
        {
            ResultPoint.SetPoint[ResultPoint.PointName.PlayerDamage] += damageCast;

            var reaction = obj.GetComponent<ArtsHitReaction>();
            if (reaction != null)
            {
                reaction.OnParticlePlay();
            }

            DamageCount.damageInput = damageCast;
            //UIバグの調査をすること
        }
        else if (obj.CompareTag("Player"))
        {
            ResultPoint.SetPoint[ResultPoint.PointName.EnemyDamage] += damageCast;
        }

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Hit);
    }

    //攻撃有効エリアかどうか
    bool OnDestroyArtsZone(GameObject obj)
    {
        var s = obj.GetComponent<DestroyArtsZoneStatus>();
        DebugLogger.Log(obj.name);
        if (s != null)
        {
            if (s.artsTypes.Contains(artsStatus.artsType) && s.particleTypes.Contains(artsStatus.type))
            {
                return true;
            }
            else return false;
        }
        else return false;
    }

    //当たり判定を出すレイヤー
    LayerMask Layer(string layerName)
    {
        if (artsStatus.artsType == ArtsStatus.ArtsType.Shot) layerNameList.Add("FlyCurse");
        if (isMapLayer) layerNameList.Add("Map");
        layerNameList.Add(layerName + "Shield");
        layerNameList.Add(layerName + "EMP");
        layerNameList.Add(layerName);
        return LayerMask.GetMask(layerNameList.ToArray());

        //if (isMapLayer)
        //{
        //    return LayerMask.GetMask("Default", "PostProcessing", "Map", layerName + "Shield", layerName);
        //}

        //return LayerMask.GetMask("Default", "PostProcessing", layerName + "Shield", layerName);
    }

    /// <summary>
    /// ヒットトリガーの取得
    /// </summary>
    public bool IsTriggerEnter
    {
        get
        {
            bool on = isTrigger;
            isTrigger = false;
            return on;
        }
    }
}
