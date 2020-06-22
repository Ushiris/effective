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
            int damage = (int)hitDamageDefault + (int)plusFormStatus;
            gameObject.GetComponent<Enemy>().life.Damage(damage);
            Debug.Log("hitCount: " + hitCount + " Damage: " + (int)hitDamageDefault + (int)plusFormStatus);
        }
        
        
    }
}
