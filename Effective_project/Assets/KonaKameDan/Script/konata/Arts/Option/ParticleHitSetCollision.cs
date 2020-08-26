using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パーティクルがヒットした場所に当たり判定を生成する
/// </summary>
public class ParticleHitSetCollision : MonoBehaviour
{
    public List<string> layerNameList = new List<string>();
    public List<Vector3> GetHitPos { get; private set; }

    ParticleSystem particle;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        GetHitPos = new List<Vector3>();
    }

    private void OnParticleCollision(GameObject obj)
    {
        int numCollisionEvents =
            particle.GetCollisionEvents(obj, collisionEvents);

        //ヒットした座標にSphereColliderを置く
        for (int i = 0; i < numCollisionEvents; i++)
        {
            //座標の検出
            var pos = collisionEvents[i].intersection;

            if (!GetHitPos.Contains(pos))
            {
                var collider = gameObject.AddComponent<SphereCollider>();
                collider.isTrigger = true;
                collider.center = gameObject.transform.InverseTransformPoint(pos);
                collider.radius = 0.5f;

                //座標を格納
                GetHitPos.Add(pos);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
    }

}
