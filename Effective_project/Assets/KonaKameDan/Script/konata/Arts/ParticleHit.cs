using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHit : MonoBehaviour
{
    public float hitDamageDefault = 3f;
    public float plusFormStatus;
    public ArtsStatus.ParticleType type;

    string hitObjTag = "Enemy";
    int hitCount = 0;

    private void Start()
    {
        var p = GetComponent<ParticleSystem>();
        var c = p.collision;

        switch (type)
        {
            case ArtsStatus.ParticleType.Player:
                c.collidesWith = Layer("Enemy");
                hitObjTag = "Enemy";
                break;

            case ArtsStatus.ParticleType.Enemy:
                c.collidesWith = Layer("Player");
                hitObjTag = "Player";
                break;

            default:break;
        }
    }

    private void OnParticleCollision(GameObject gameObject)
    {
        hitCount++;
        
        if (gameObject.tag == hitObjTag)
        {
            float damage = hitDamageDefault * plusFormStatus;
            int damageCast = Mathf.CeilToInt(damage);

            gameObject.GetComponent<Enemy>().life.Damage(damageCast);
            Debug.Log("hitCount: " + hitCount + "damage: " + damage + " damageCast: " + damageCast + " hitDamageDefault: " + hitDamageDefault);


            //SE
            SE_Manager.SePlay(SE_Manager.SE_NAME.Hit);
        }
        
        
    }

    //当たり判定を出すレイヤー
    LayerMask Layer(string layerName)
    {
        return LayerMask.GetMask("Default", "PostProcessing", "Map", layerName);
    }
}
