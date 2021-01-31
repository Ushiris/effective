using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHit : MonoBehaviour
{
    void OnParticleCollision(GameObject col)
    {
        if (col.tag == "Enemy")
        {
            DamageCount.Damage(col);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            DamageCount.Damage(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            DamageCount.Damage(collision.gameObject);
        }
    }
}