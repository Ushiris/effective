using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum MoveState
    {
        Chase,
        Stay
    }
    public MoveState move = MoveState.Stay;
}
