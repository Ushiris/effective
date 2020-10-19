using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoveState = EnemyState.MoveState;
using Enchants = EnemyState.Enchants;
using System;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyState))]
public class EnemyBrain :EnemyBrainBase
{
    void Start()
    {
        base.Start();
    }

    private void LateUpdate()
    {
        if (state.move == MoveState.Confuse)
        {
            return;
        }

        if (FindFlag())
        {
            state.move = MoveState.Chase;
            Think();
        }
        else
        {
            state.move = MoveState.Stay;
        }
    }
}
