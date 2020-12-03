using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id279_ChargeDrive : MonoBehaviour
{
    [SerializeField] float force = 100f;

    // Start is called before the first frame update
    void Start()
    {
        var obj = PlayerManager.GetManager.GetPlObj;
        Arts_Process.RbMomentMove(obj, force);
        Destroy(gameObject, 0.5f);
    }
}
