using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObjectHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)// 当たった瞬間
    {
        //DebugLogger.Log("当たってるよ！");
    }
    private void OnTriggerExit(Collider other)// 離れた瞬間
    {
        //DebugLogger.Log("離れた！");
    }
}
