using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrainShot : EnemyBrainBase
{
    private new void Start()
    {
        base.Start();
        navMesh.isStopped = true;
    }

    private void LateUpdate()
    {
        navMesh.isStopped = true;
        if (FindFlag())
        {
            state.move = EnemyState.MoveState.Chase;
            Think();
        }
        else
        {
            state.move = EnemyState.MoveState.Stay;
        }
    }
}
