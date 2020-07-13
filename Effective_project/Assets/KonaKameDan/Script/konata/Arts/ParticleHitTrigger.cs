using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHitTrigger : MonoBehaviour
{
    public float hitDamageDefault = 3f;
    public float plusFormStatus;
    public ArtsStatus.ParticleType type;

    string hitObjTag = "Enemy";

    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case ArtsStatus.ParticleType.Player:
                hitObjTag = "Enemy";
                break;

            case ArtsStatus.ParticleType.Enemy:
                hitObjTag = "Player";
                break;

            default: break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.gameObject.tag == hitObjTag)
        {
            float damage = hitDamageDefault * plusFormStatus;
            int damageCast = Mathf.CeilToInt(damage);

            //gameObject.GetComponent<Enemy>().life.Damage(damageCast);
        }
    }
}
