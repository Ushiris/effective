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
    Vector3 jump;
    Vector3 move;

    int layerMask;
    int jumpCount;

    [SerializeField] Status status;
    [SerializeField] float speed;
    [SerializeField] float slowSpeed = 10f;
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float jumpPower = 15f;
    [SerializeField] int maxJumpCount = 2;
    [SerializeField] GameObject jumpColliderPos;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        speed = slowSpeed;

        //地面判定のレイヤー取得
        layerMask = LayerMask.GetMask("Map");

        //重力の変更
        Physics.gravity = new Vector3(0, -20, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //プレイヤーが回転した時、動きもそれに合わせるための式
        float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f);
        dirVertical = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir)) * Input.GetAxis("Vertical") * speed;
        dirHorizontal = new Vector3(-Mathf.Cos(angleDir), 0, Mathf.Sin(angleDir)) * Input.GetAxis("Horizontal") * speed;

        //ジャンプ
        jump = Vector3.up * JumpTrigger(maxJumpCount) * jumpPower;

        //入力したときにカメラの向きを基準とした動き
        move = dirVertical + -dirHorizontal + jump;

        //速度の変更
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = dashSpeed;
        }
        else
        {
            speed = status.GetMoveSpeed + slowSpeed;
        }

        //移動
        Vector3 v = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        Vector3 force = new Vector3(speed * (move.x - v.x), move.y, speed * (move.z - v.z));
        rb.AddForce(force);
    }

    //ジャンプ制限付きトリガーチェック
    float JumpTrigger(int maxJumpCount = 1)
    {
        //着地した時ジャンプできる回数をリセットする
        if (IsLanding()) jumpCount = 0;

        if (jumpCount != maxJumpCount)
        {
            jumpCount++;
            return Input.GetAxis("Jump");
        }
        else return 0;
    }

    //着地した時、真を返す
    bool IsLanding()
    {
        var pos = jumpColliderPos.transform.position;
        Collider[] ground = Physics.OverlapSphere(pos, 0.1f, layerMask);
        if (ground != null)
        {
            if (ground.Length != 0)
            {
                return true;
            }
        }
        return false;
    }
}
