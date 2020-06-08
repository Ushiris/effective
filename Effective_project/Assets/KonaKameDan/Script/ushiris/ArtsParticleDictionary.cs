using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticleDictionary : Serialize.TableBase<string, ParticleSystem, Name2Particle> { }

[System.Serializable]
public class Name2Particle : Serialize.KeyAndValue<string, ParticleSystem>
{
    public Name2Particle(string key, ParticleSystem value) : base(key, value) { }
}

public class ArtsParticleDictionary : SingletonMonoBehaviour<ArtsParticleDictionary>
{
    [SerializeField] public ParticleDictionary particle;
}
