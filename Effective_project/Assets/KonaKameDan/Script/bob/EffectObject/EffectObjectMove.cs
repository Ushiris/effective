using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObjectMove : MonoBehaviour
{
    [SerializeField] float sin;// 上下運動用
    private float oldPositionY;// 初めの高さ
    public float rotationSpeed;// 回転スピード
    public float sinAdjustment;// 揺れ幅調整
    public bool isStopMove;

    private void Start()
    {
        oldPositionY = transform.position.y;
    }
    void Update()
    {
        sin = Mathf.Sin(Time.time) * sinAdjustment;
        transform.Rotate(new Vector3(0.0f, rotationSpeed, 0.0f));
        if (!isStopMove)
        {
            transform.position = new Vector3(transform.position.x, sin + oldPositionY + sinAdjustment, transform.position.z);
        }
    }
}
