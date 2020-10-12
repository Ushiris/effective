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
    string notHitObjTag;

    static readonly List<string> kExceptionTags = new List<string>()
    {
        "NearEnemyPos","BossZone","EnemySpawnZone"
    };

    private void Start()
    {
        if (artsStatus == null) return;
        isTrigger = false;
        switch (artsStatus.type)
        {
            case ArtsStatus.ParticleType.Player:
                hitObjTag = "Enemy";
                notHitObjTag = "Player";
                break;
            case ArtsStatus.ParticleType.Enemy:
                hitObjTag = "Player";
                notHitObjTag = "Enemy";
                break;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        var tag = other.tag;
        if (IsCheckTag(tag))
        {
            InstantParticle(other);
            isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.gameObject.tag;
        if (IsCheckTag(tag))
        {
            InstantParticle(other.gameObject);
            isTrigger = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var tag = collision.gameObject.tag;
        if (IsCheckTag(tag))
        {
            InstantParticle(collision.gameObject);
            isTrigger = true;
        }
    }

    //エフェクト生成処理
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

    //Tagの判定
    bool IsCheckTag(string tag)
    {
        if (tag != notHitObjTag && kExceptionTags.Contains(tag))
        {
            if (tag == hitObjTag || isAllHit)
            {
                DebugLogger.Log("tag:" + tag);
                return true;
            }
        }
        return false;
    }
}
