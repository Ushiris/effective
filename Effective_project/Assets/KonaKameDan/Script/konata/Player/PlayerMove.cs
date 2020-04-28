﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの移動
/// </summary>
public class PlayerMove : MonoBehaviour
{

    Rigidbody rb;

    Vector3 dirVertical;
    Vector3 dirHorizontal;

    [SerializeField] float speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが回転した時、動きもそれに合わせるための式
        float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f);
        dirVertical = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir)) * Input.GetAxis("Vertical") * speed;
        dirHorizontal = new Vector3(-Mathf.Cos(angleDir), 0, Mathf.Sin(angleDir)) * Input.GetAxis("Horizontal")  * speed;

        //入力したときにカメラの向きを基準とした動き
        Vector3 move = dirVertical + -dirHorizontal;



        //移動
        rb.AddForce(speed * (move - rb.velocity));
    }
}