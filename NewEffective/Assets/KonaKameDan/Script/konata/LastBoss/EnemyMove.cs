using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public enum Move
    {
        CenterLeftCircle, CenterRightCircle,
        PlayerLeftCircle, PlayerRightCircle,

        Idol, GoToPlayer, GoToCenter
    }

    [SerializeField] Transform centerPos;
    [SerializeField] Transform playerPos;
    [SerializeField] float limitDis = 20f;
    [SerializeField] float jumpPower = 30f;

    [SerializeField] float speed = 10f;

    [SerializeField] Move moveType;
    Rigidbody rb;
    bool isJump;

    public float GetCenterDis { get; private set; }
    public float GetPlayerDis { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var pos = rb.position;
        var centerDis = Vector3.Distance(pos, centerPos.position);
        var playerDis = Vector3.Distance(pos, playerPos.position);

        GetCenterDis = centerDis;
        GetPlayerDis = playerDis;

        if (centerDis < limitDis)
        {
            switch (moveType)
            {
                case Move.CenterLeftCircle:
                    pos = CircleMove(centerPos, -speed);
                    break;

                case Move.CenterRightCircle:
                    pos = CircleMove(centerPos, speed);
                    break;

                case Move.PlayerLeftCircle:
                    pos = CircleMove(playerPos, -speed);
                    break;

                case Move.PlayerRightCircle:
                    pos = CircleMove(playerPos, speed);
                    break;

                case Move.Idol:
                    break;

                case Move.GoToPlayer:
                    pos = AdvanceMove(playerPos, speed / 5);
                    break;

                case Move.GoToCenter:
                    pos = AdvanceMove(centerPos, speed / 5);
                    break;

                default: break;
            }
        }
        else
        {
            AdvanceMove(centerPos, speed / 5);
        }

        if (isJump)
        {
            rb.AddForce(Vector3.up * jumpPower);
            isJump = false;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        //if (Input.GetKeyDown(KeyCode.Space)) OnJump();
        rb.MovePosition(pos);
    }

    Vector3 CircleMove(Transform center, float speed)
    {
        var angleAxis = Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.up);
        var pos = rb.position;

        pos -= center.position;
        pos = angleAxis * pos;
        pos += center.position;

        return pos;
    }

    Vector3 AdvanceMove(Transform center,float speed)
    {
        var pos = rb.position;
        var dis = pos - center.position;

        pos -= dis.normalized * speed * Time.deltaTime;

        return pos;
    }

    public void OnJump()
    {
        isJump = true;
    }

    public void SetMoveType(Move type, float speed)
    {
        this.speed = speed;
        moveType = type;
    }

    public void SetPos(Transform player, Transform center)
    {
        playerPos = player;
        centerPos = center;
    }
}
