using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHit : MonoBehaviour
{
    public float hitDamageDefault = 3f;
    public float plusFormStatus;
    public string hitObjTag = "Enemy";
    int hitCount = 0;

    private void OnParticleCollision(GameObject gameObject)
    {
        hitCount++;
        if (gameObject.tag == hitObjTag)
        {
            float damage = hitDamageDefault * plusFormStatus;
            int damageCast = Mathf.CeilToInt(damage);

            gameObject.GetComponent<Enemy>().life.Damage(damageCast);
            Debug.Log("hitCount: " + hitCount + " Damage: " + damageCast + " hitDamageDefault: " + hitDamageDefault);


            //SE
            SE_Manager.SePlay(SE_Manager.SE_NAME.Hit);
        }
        
        
    }
}
