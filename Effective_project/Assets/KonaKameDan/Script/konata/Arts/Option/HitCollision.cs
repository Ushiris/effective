using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 何かに当たった場合
/// </summary>
public class HitCollision : MonoBehaviour
{
    public bool GetOnTrigger { get; private set; }
    public List<string> tags = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (tags.Count != 0)
        {
            
            if (tags.Contains(other.gameObject.tag))
            {
                GetOnTrigger = true;
            }
        }
    }
}
