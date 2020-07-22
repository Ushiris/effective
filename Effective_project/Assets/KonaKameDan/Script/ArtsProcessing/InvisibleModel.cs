using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// モデルと当たり判定をなくすかどうか
/// </summary>
public class InvisibleModel : MonoBehaviour
{
    [SerializeField] GameObject model;
    [SerializeField] Collider collider;

    private void Awake()
    {
        model.SetActive(true);
        collider.enabled = true;
    }

    public void Invisible(bool isActive)
    {
        model.SetActive(isActive);
        collider.enabled = isActive;
    }
}
