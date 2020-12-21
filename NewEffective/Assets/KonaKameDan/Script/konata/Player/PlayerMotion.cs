using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    public enum MoveVector : int
    {
        Forward = 0, Back = 1, Right = 2, Left = 3, stop = 4
    }

    public struct PlayerMoveInfo
    {
        public bool
            IsDefault,
            IsRunning;
        public MoveVector
            moveTo;
    }

    Animator animator;
    PlayerMoveInfo info;

    /// <summary>
    /// プレイヤーの走るモーションを起動
    /// </summary>
    public static bool OnPlayerRunningMotion { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerMove pl_move = PlayerManager.GetManager.GetPlObj.GetComponent<PlayerMove>();
        pl_move.OnNearGround.AddListener(() => animator.SetTrigger("OnGround"));
        pl_move.OnJumpBegin.AddListener(() => animator.SetTrigger("OnJump"));
    }

    private void Update()
    {
        if (Input.GetAxis("Vertical") != 0 ||
            Input.GetAxis("Horizontal") != 0 ||
            OnPlayerRunningMotion)
        {
            info.IsDefault = false;
            info.IsRunning = true;
            animator.SetBool("IsDefault", false);
            animator.SetBool("IsRunning", true);

            if (Input.GetAxis("Vertical") > 0f)
            {
                info.moveTo = MoveVector.Forward;
            }
            else if (Input.GetAxis("Vertical") < 0f)
            {
                info.moveTo = MoveVector.Back;
            }
            else if (Input.GetAxis("Horizontal") > 0f)
            {
                info.moveTo = MoveVector.Right;
            }
            else if (Input.GetAxis("Horizontal") < 0f)
            {
                info.moveTo = MoveVector.Left;
            }
        }
        else
        {
            info.IsDefault = true;
            info.IsRunning = false;
            info.moveTo = MoveVector.stop;
            animator.SetBool("IsDefault", true);
            animator.SetBool("IsRunning", false);
        }
        animator.SetInteger("MoveVector", (int)info.moveTo);
    }
}
