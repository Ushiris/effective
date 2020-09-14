using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パーティクルが当たった場合、オブジェクトをスポーンさせる
/// </summary>
public class ParticleHitPlayExplosion : MonoBehaviour
{
    public enum Mode { My, You }

    public GameObject playParticle;
    public Transform parent;
    public float particleLostTime = 3f;
    public ArtsStatus artsStatus;

    public Mode mode = Mode.You;
    public bool isAllHit = false;

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
        if (other.tag == hitObjTag || isAllHit)
        {
            InstantParticle(other);
            isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == hitObjTag || isAllHit)
        {
            InstantParticle(other.gameObject);
            isTrigger = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == hitObjTag || isAllHit)
        {
            InstantParticle(collision.gameObject);
            isTrigger = true;
        }
    }

    void InstantParticle(GameObject target)
    {
        var obj = Instantiate(playParticle, parent);
        switch (mode)
        {
            case Mode.My: obj.transform.position = transform.position; break;
            case Mode.You: obj.transform.position = target.transform.position; break;
            default: break;
        }
        Destroy(obj, particleLostTime);
    }
}
