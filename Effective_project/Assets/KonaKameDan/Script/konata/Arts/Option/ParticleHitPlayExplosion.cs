using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHitPlayExplosion : MonoBehaviour
{
    public GameObject playParticle;

    private void OnParticleCollision(GameObject other)
    {
        InstantParticle();
    }

    void InstantParticle()
    {
        var obj = Instantiate(playParticle, transform);
    }
}
