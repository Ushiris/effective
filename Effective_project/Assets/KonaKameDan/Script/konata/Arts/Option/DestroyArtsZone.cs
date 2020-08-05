using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 特定の種類のArtsを消す   
/// </summary>
public class DestroyArtsZone : MonoBehaviour
{
    public List<ArtsStatus.ArtsType> artsTypes;
    public List<ArtsStatus.ParticleType> particleTypes;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other);
        var s = other.GetComponent<ArtsStatus>();
        if (s != null)
        {
            if (artsTypes.Contains(s.artsType) && particleTypes.Contains(s.type))
            {
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
        }
    }
}
