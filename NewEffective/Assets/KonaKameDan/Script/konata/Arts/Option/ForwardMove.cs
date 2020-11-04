using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 前進させる
/// </summary>
public class ForwardMove : MonoBehaviour
{
    public bool isStart = false;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;

        var pos = transform.localPosition;
        pos.y += speed * Time.deltaTime;
        transform.localPosition = pos;
    }
}
