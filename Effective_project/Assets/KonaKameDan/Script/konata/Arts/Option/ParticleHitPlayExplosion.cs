using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パーティクルが当たった場合、オブジェクトをスポーンさせる
/// </summary>
public class ParticleHitPlayExplosion : MonoBehaviour
{
    public GameObject playParticle;
    public Transform parent;
    public float particleLostTime = 3f;
    public ArtsStatus artsStatus;

    public bool isTrigger { get; private set; }

    string hitObjTag;

    private void Start()
    {
        if (artsStatus == null) return;
        isTrigger = false;
        switch (artsStatus.type)
        {
            case ArtsStatus.ParticleType.Player:
                hitObjTag = "Enemy";
                break;
            case ArtsStatus.ParticleType.Enemy:
                hitObjTag = "Player";
                break;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == hitObjTag)
        {
            InstantParticle(other);
            isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == hitObjTag)
        {
            InstantParticle(other.gameObject);
            isTrigger = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == hitObjTag)
        {
            InstantParticle(collision.gameObject);
            isTrigger = true;
        }
    }

    void InstantParticle(GameObject target)
    {
        var obj = Instantiate(playParticle, parent);
        obj.transform.position = target.transform.position;
        Destroy(obj, particleLostTime);
    }
}
