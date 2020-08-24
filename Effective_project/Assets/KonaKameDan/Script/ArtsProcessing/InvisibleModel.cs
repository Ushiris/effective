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
        if (isActive) model.SetActive(false);
        else model.SetActive(true);
        life.damageGuard = isActive;
    }
}
