using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObjectMove : MonoBehaviour
{
    [SerializeField] float sin;// 上下運動用
    private float oldPositionY;// 初めの高さ
    public float rotationSpeed;// 回転スピード
    public float sinAdjustment;// 揺れ幅調整

    private void Start()
    {
        oldPositionY = transform.localPosition.y;
    }
    void Update()
    {
        sin = Mathf.Sin(Time.time) * sinAdjustment;
        transform.Rotate(new Vector3(0.0f, rotationSpeed, 0.0f));
        var pos = transform.localPosition;
        transform.localPosition = new Vector3(pos.x, sin + oldPositionY + sinAdjustment, pos.z);
    }
}
