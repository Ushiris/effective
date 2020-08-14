using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// モデルと当たり判定をなくすかどうか
/// </summary>
public class InvisibleModel : MonoBehaviour
{
    [SerializeField] GameObject model;
    [SerializeField] Life life;

    private void Awake()
    {
        model.SetActive(true);
        life.damageGuard = false;
    }

    public void Invisible(bool isActive)
    {
        model.SetActive(isActive);
        if (isActive) life.damageGuard = false;
        else life.damageGuard = true;
    }
}
