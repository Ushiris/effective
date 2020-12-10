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
    public delegate void Action();
    public Action OnExplosion;

    public bool isTrigger { get; private set; }

    string hitObjTag;
    string notHitObjTag;
    List<string> layerNameList = new List<string>();

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
        OnExplosion += () => { };
    }

    private void OnParticleCollision(GameObject other)
    {
        var tag = other.tag;
        if (IsCheckTag(tag) || IsCheckLayer(other))
        {
            InstantParticle(other);
            isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.gameObject.tag;
        if (IsCheckTag(tag) || IsCheckLayer(other.gameObject))
        {
            InstantParticle(other.gameObject);
            isTrigger = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var tag = collision.gameObject.tag;
        if (IsCheckTag(tag) || IsCheckLayer(collision.gameObject))
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
        OnExplosion();
        Destroy(obj, particleLostTime);
    }

    //Tagの判定
    bool IsCheckTag(string tag)
    {
        if (tag != notHitObjTag && !kExceptionTags.Contains(tag))
        {
            if (tag == hitObjTag || isAllHit)
            {
                DebugLogger.Log("tag:" + tag);
                return true;
            }
        }
        return false;
    }

    //シールドとEMPで弾くよう
    bool IsCheckLayer(GameObject obj)
    {

        if (artsStatus.type == ArtsStatus.ParticleType.Player)
        {
            if (obj.layer == Layer("Enemy")) return true;
            else return false;
        }
        if (artsStatus.type == ArtsStatus.ParticleType.Enemy)
        {
            if (obj.layer == Layer("Player")) return true;
            else return false;
        }
        else return false;
    }

    LayerMask Layer(string layerName)
    {
        if (artsStatus.artsType == ArtsStatus.ArtsType.Shot) layerNameList.Add("FlyCurse");
        layerNameList.Add(layerName + "Shield");
        layerNameList.Add(layerName + "EMP");
        return LayerMask.GetMask(layerNameList.ToArray());
    }
}
