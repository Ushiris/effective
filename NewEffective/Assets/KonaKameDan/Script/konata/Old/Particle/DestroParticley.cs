using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パーティクルが表示されていない場合このオブジェクトを消す
/// </summary>
public class DestroParticley : MonoBehaviour
{
    ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var me = particle.emission;
        if (particle.particleCount == 0 && !me.enabled)
        {
            Destroy(gameObject);
        }
    }
}
