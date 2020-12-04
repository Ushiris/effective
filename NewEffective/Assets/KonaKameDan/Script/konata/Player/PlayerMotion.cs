﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    Animator animator;

    /// <summary>
    /// プレイヤーの走るモーションを起動
    /// </summary>
    public static bool OnPlayerRunningMotion { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0 ||
            Input.GetAxis("Horizontal") != 0 ||
            OnPlayerRunningMotion)
        {
            animator.SetBool("IsDefault", false);
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsDefault", true);
            animator.SetBool("IsRunning", false);
        }
    }
}
