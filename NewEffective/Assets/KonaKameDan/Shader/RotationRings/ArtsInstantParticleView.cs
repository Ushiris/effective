using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtsInstantParticleView : MonoBehaviour
{
    [SerializeField] new ParticleSystem[] particleSystem;

    /// <summary>
    /// Artsを放つと出る演出
    /// </summary>
    public void SetParticlePlay()
    {
        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.CastArts);

        for(int i = 0; i < particleSystem.Length; i++)
        {
            particleSystem[i].Play();
        }
    }

}
