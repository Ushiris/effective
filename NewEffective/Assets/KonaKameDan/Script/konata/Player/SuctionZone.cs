using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuctionZone : MonoBehaviour
{
    static readonly string tga = "EffectObject";
    private void OnTriggerEnter(Collider collision)
    {
        var obj = collision.gameObject;

        var objGoToTarget = obj.GetComponent<ObjGoToTarget>();
        objGoToTarget.enabled = true;
        objGoToTarget.isGo = true;
        objGoToTarget.targetPos = transform;
    }
}
