using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホーミング弾
/// </summary>
public class HomingParticle : MonoBehaviour
{
    public Transform target;

    public float force = 10.0f;

    new ParticleSystem particleSystem;
    ParticleSystem.Particle[] particles;

    ParticleSystem.MainModule particleSystemMainModule;

    // Start is called before the first frame update
    void Start()
    {
        //宣言
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemMainModule = particleSystem.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //パーティクルの最大値
        int maxParticles = particleSystemMainModule.maxParticles;

        //全てのパーティクルを入れる
        particles = new ParticleSystem.Particle[maxParticles];

        //パーティクルを取得
        particleSystem.GetParticles(particles);

        //速度の計算
        float forceDeltaTime = force * Time.deltaTime;

        //ターゲットの座標
        Vector3 targetTransformedPosition = target.position;

        //現在出ているパーティクルの数
        int particleCount = particleSystem.particleCount;

        //パーティクル全ての位置をターゲットに向かわせる
        for (int i = 0; i < particleCount; i++)
        {
            //方向
            Vector3 directionToTarget = Vector3.Normalize(targetTransformedPosition - particles[i].position);
            //速度
            Vector3 seekForce = directionToTarget * forceDeltaTime;
            //パーティクルに代入
            particles[i].velocity += seekForce;
        }

        //パーティクルに反映
        particleSystem.SetParticles(particles, particleCount);
    }
}
