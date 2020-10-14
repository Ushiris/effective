using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 何かに当たった場合
/// </summary>
public class HitCollision : MonoBehaviour
{
    public bool GetOnTrigger { get; private set; }
    List<string> tags = new List<string>()
    {
        "Ground"
    };

    private void OnEnable()
    {
        GetOnTrigger = false;
    }

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

    /// <summary>
    /// 反応するTagを新しく追加
    /// </summary>
    /// <param name="tag"></param>
    public void SetTags(string tag)
    {
        if (!tags.Contains(tag)) tags.Add(tag);
    }
}
