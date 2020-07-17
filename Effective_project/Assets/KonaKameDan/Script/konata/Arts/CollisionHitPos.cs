using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 当たった位置オブジェクトの
/// </summary>
public class CollisionHitPos : MonoBehaviour
{
    public Vector3? hitPos { get; private set; }

    private void Start()
    {
        hitPos = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitPos = transform.position;
    }
}
