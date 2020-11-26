using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrainShot : EnemyBrainBase
{
    private new void Start()
    {
        base.Start();
    }

    private void LateUpdate()
    {
        if (FindFlag())
        {
            state.move = EnemyState.MoveState.Chase;
            Think();
        }
        else
        {
            state.move = EnemyState.MoveState.Stay;
        }
        rb.velocity = Vector3.zero;
    }
}
