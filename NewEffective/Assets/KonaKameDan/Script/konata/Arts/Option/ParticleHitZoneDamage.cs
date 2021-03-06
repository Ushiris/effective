﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 継続ダメージ処理　
/// </summary>
public class ParticleHitZoneDamage : MonoBehaviour
{
    public float hitDamageDefault = 3f;
    public float plusFormStatus;
    public ArtsStatus artsStatus;
    public bool isMapLayer;

    public bool isParticleCollision = true;

    public static bool isAllDamageEnable { get; set; } = true;

    string hitObjTag;

    List<string> layerNameList = new List<string>()
    {
        "Default", "PostProcessing", "Map"
    };

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        switch (artsStatus.type)
        {
            case ArtsStatus.ParticleType.Player:
                gameObject.layer = LayerMask.NameToLayer("PlayerArts");
                hitObjTag = "Enemy";

                //ダメージUIを出すやつ
                gameObject.AddComponent<DamageHit>();
                break;

            case ArtsStatus.ParticleType.Enemy:
                gameObject.layer = LayerMask.NameToLayer("EnemyArts");
                hitObjTag = "Player";
                break;

            default: break;
        }

        timer = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime * 10;
        if (!OnDestroyArtsZone(other.gameObject))
        {
            var damageScale = Mathf.FloorToInt(timer);
            if (other.gameObject.tag == hitObjTag && damageScale % 5 == 0)
            {
                Damage(other.gameObject);
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
        if (!isAllDamageEnable && obj == null) return;

        float damage = hitDamageDefault * plusFormStatus;
        int damageCast = Mathf.CeilToInt(damage);

        var life = obj.GetComponent<Life>();
        if (life != null) life.Damage(damageCast);
        DebugLogger.Log("name: " + artsStatus.myObj.name + " damage: " + damage + " damageCast: " + damageCast + " hitDamageDefault: " + hitDamageDefault);

        //UI&Point
        if (obj.tag == "Enemy")
        {
            ResultPoint.SetPoint[ResultPoint.PointName.PlayerDamage] += damageCast;

            var reaction = obj.GetComponent<ArtsHitReaction>();
            if (reaction != null)
            {
                reaction.OnParticlePlay();
            }
            DamageCount.damageInput = damageCast;
        }
        else if(obj.tag=="Player")
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
    }
}
