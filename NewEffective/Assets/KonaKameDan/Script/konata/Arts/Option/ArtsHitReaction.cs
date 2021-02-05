using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtsHitReaction : MonoBehaviour
{
    [SerializeField] new ParticleSystem[] particleSystem;

    /// <summary>
    /// Artsを放つと出る演出
    /// </summary>
    public void OnParticlePlay()
    {
        for (int i = 0; i < particleSystem.Length; i++)
        {
            particleSystem[i].Play();
        }
    }
}
