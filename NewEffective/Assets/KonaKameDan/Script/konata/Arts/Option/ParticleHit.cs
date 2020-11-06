﻿using System.Collections;
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

    string hitObjTag;
    bool isTrigger;

    List<string> layerNameList = new List<string>()
    {
        "Default", "PostProcessing", "Map"
    };

    private void Start()
    {
        //ダメージUIを出すやつ
        gameObject.AddComponent<DamageHit>();

        var p = GetComponent<ParticleSystem>();
        var c = p.collision;

        if (!isParticleCollision) p = null;

        switch (artsStatus.type)
        {
            case ArtsStatus.ParticleType.Player:
                if (p != null) c.collidesWith = Layer("Enemy");
                gameObject.layer = LayerMask.NameToLayer("PlayerArts");
                hitObjTag = "Enemy";
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
        if (obj.tag == hitObjTag)
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
        float damage = hitDamageDefault * plusFormStatus;
        int damageCast = Mathf.CeilToInt(damage);

        var life = obj.GetComponent<Life>();
        if (life != null) life.Damage(damageCast);
        DebugLogger.Log("name: " + artsStatus.myObj.name + " damage: " + damage + " damageCast: " + damageCast + " hitDamageDefault: " + hitDamageDefault);

        //ノックバック判定
        if (obj.tag == "Enemy" && isEnemyKnockBack)
        {
            var enemy = obj.GetComponent<Enemy>();
            if(enemy!=null)enemy.KnockBack();
        }

        //UI
        if (obj.tag == "Enemy")
        {
            DamageCount.damageInput = damageCast;
            //UIバグの調査をすること
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