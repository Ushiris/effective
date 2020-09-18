﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum MoveState
    {
        Chase,
        Stay,
        Stan
    }
    public MoveState move = MoveState.Stay;
}
