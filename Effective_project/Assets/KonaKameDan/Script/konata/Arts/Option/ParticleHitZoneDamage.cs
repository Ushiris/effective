using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 継続ダメージ処理　
/// </summary>
[RequireComponent(typeof(DamageHit))]
public class ParticleHitZoneDamage : MonoBehaviour
{
    public float hitDamageDefault = 3f;
    public float plusFormStatus;
    public ArtsStatus artsStatus;
    public bool isMapLayer;

    public bool isParticleCollision = true;

    string hitObjTag;
    bool isTrigger;

    List<string> layerNameList = new List<string>()
    {
        "Default", "PostProcessing", "Map"
    };

    float timer;

    // Start is called before the first frame update
    void Start()
    {
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

        timer = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime * 10;
        if (!OnDestroyArtsZone(other.gameObject))
        {
            if (other.CompareTag(hitObjTag) && (int)timer % 5 == 0)
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

    //ダメージの処理
    void Damage(GameObject enemy)
    {
        float damage = hitDamageDefault * plusFormStatus;
        int damageCast = Mathf.CeilToInt(damage);

        var life = enemy.GetComponent<Life>();
        if (life != null) life.Damage(damageCast);
        DebugLogger.Log("name: " + artsStatus.myObj.name + " damage: " + damage + " damageCast: " + damageCast + " hitDamageDefault: " + hitDamageDefault);

        //UI
        if(enemy.tag== "Enemy") DamageCount.damageInput = damageCast;

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
