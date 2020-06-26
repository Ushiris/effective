using System.Collections;
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

    [SerializeField] float speed;
    [SerializeField] float slowSpeed = 10f;
    [SerializeField] float dashSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        speed = slowSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //プレイヤーが回転した時、動きもそれに合わせるための式
        float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f);
        dirVertical = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir)) * Input.GetAxis("Vertical") * speed;
        dirHorizontal = new Vector3(-Mathf.Cos(angleDir), 0, Mathf.Sin(angleDir)) * Input.GetAxis("Horizontal")  * speed;

        //入力したときにカメラの向きを基準とした動き
        Vector3 move = dirVertical + -dirHorizontal;

        //速度の変更
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = dashSpeed;
        }
        else 
        {
            speed = slowSpeed;
        }



        //移動
        rb.AddForce(speed * (move - rb.velocity));
    }
}
