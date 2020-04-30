using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObjectMove : MonoBehaviour
{
    public float rotationSpeed;
    [SerializeField] float sin;// 上下運動用
    void Update()
    {
        sin = Mathf.Sin(Time.time);
        transform.Rotate(new Vector3(0.0f, rotationSpeed, 0.0f));
        transform.position = new Vector3(transform.position.x, sin, transform.position.z);
    }
}
