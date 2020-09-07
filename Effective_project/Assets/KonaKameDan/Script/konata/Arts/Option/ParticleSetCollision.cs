using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パーティクル生成位置に当たり判定を生成する
/// </summary>
public class ParticleSetCollision : MonoBehaviour
{
    public List<Vector3> GetPos { get; private set; }

    int maxParticles;
    ParticleSystem particle;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    // Start is called before the first frame update
    void Start()
    {
        GetPos = new List<Vector3>();

        particle = GetComponent<ParticleSystem>();    
    }

    // Update is called once per frame
    void Update()
    {
        // パーティクルの最大値
        ParticleSystem.MainModule particleSystemMainModule;
        particleSystemMainModule = particle.main;
        int maxParticles = particleSystemMainModule.maxParticles;

        if (GetPos.Count == maxParticles) return;

        //全てのパーティクルを入れる
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[maxParticles];

        //パーティクルを取得
        particle.GetParticles(particles);

        //現在出ているパーティクルの数
        int particleCount = particle.particleCount;

        //SphereColliderを置く
        for (int i = 0; i < particleCount; i++)
        {
            var pos = particles[i].position;
            if (!GetPos.Contains(pos))
            {
                var collider = gameObject.AddComponent<SphereCollider>();
                collider.isTrigger = true;
                collider.center = pos;
                collider.radius = 0.5f;

                //座標を格納
                GetPos.Add(pos);
            }
        }
    }
}
