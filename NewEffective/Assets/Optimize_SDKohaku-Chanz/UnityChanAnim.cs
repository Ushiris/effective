﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanAnim : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
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
