using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    public class PlayerMoveInfo
    {
        public bool
            IsDefault = true,
            IsRunning = false;
        public bool[]
            InputVector
        { get; private set; } = new bool[4] { false, false, false, false };

        public void SetInput(bool[] input)
        {
            InputVector = input;
        }
    }

    Animator animator;
    PlayerMoveInfo info = new PlayerMoveInfo();

    /// <summary>
    /// プレイヤーの走るモーションを起動
    /// </summary>
    public static bool OnPlayerRunningMotion { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerMove pl_move = NewMap.GetPlayerObj.GetComponent<PlayerMove>();
        pl_move.OnNearGround.AddListener(() => animator.SetTrigger("OnGround"));
        pl_move.OnJumpBegin.AddListener(() => animator.SetTrigger("OnJump"));
    }

    private void Update()
    {
        bool[] input = new bool[4] { false, false, false, false };
        if (Input.GetAxis("Vertical") != 0 ||
            Input.GetAxis("Horizontal") != 0 ||
            OnPlayerRunningMotion)
        {
            info.IsDefault = false;
            info.IsRunning = true;
            animator.SetBool("IsDefault", false);
            animator.SetBool("IsRunning", true);

            input[0] = Input.GetAxis("Vertical") > 0f;//前
            input[1] = Input.GetAxis("Vertical") < 0f;//後ろ
            input[2] = Input.GetAxis("Horizontal") > 0f;//右
            input[3] = Input.GetAxis("Horizontal") < 0f;//左
            info.SetInput(input);
        }
        else
        {
            info.IsDefault = true;
            info.IsRunning = false;
            info.SetInput(input);
            animator.SetBool("IsDefault", true);
            animator.SetBool("IsRunning", false);
        }
        animator.SetInteger("MoveVector", MoveVector(info.InputVector));
        //DebugLogger.Log(MoveVector(info.InputVector));
    }

    //前=0で時計回りに8方位。8は停止。
    int MoveVector(bool[] input)
    {
        if (input[0] && input[1])
        {
            input[0] = input[1] = false;
        }
        if (input[2] && input[3])
        {
            input[2] = input[3] = false;
        }

        //前進
        if (input[0])
        {
            if (input[2])
            {
                return 1;
            }
            else if (input[3])
            {
                return 7;
            }

            return 0;
        }
        //後退
        else if (input[1])
        {
            if (input[2])
            {
                return 3;
            }
            else if (input[3])
            {
                return 5;
            }

            return 4;
        }
        else if (input[2]) return 2;
        else if (input[3]) return 6;

        return 8;
    }
}
