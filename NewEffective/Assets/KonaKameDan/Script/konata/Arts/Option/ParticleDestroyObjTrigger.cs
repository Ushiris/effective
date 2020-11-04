using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パーティクルがヒットするとオブジェクトを消す
/// </summary>
public class ParticleDestroyObjTrigger : MonoBehaviour
{
    [SerializeField] GameObject destroyObj;

    private void OnParticleCollision(GameObject other)
    {
        Destroy(destroyObj);
    }
}
