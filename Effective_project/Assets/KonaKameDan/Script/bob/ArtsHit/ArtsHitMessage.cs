using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtsHitMessage : MonoBehaviour
{
    public void HitMessage()// エネミーに攻撃が当たった時に呼ばれる
    {
        Debug.Log("エネミー「痛い…やめろぉ！」");
    }
}
