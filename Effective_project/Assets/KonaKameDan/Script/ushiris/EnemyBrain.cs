using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoveState = EnemyState.MoveState;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBrain :EnemyBrainBase
{
    new void Start()
    {
        base.Start();

        var rand = Random.Range(1, 10);
        if (rand >= 8)
        {
            AIset(FindAItype.Commander);
        }
        else
        {
            AIset(FindAItype.Soldier);

            if (rand < 4)
            {
                AIset(StayAItype.Ambush);
            }
            else if (rand > 6)
            {
                AIset(StayAItype.Ninja);
            }
            else
            {
                AIset(StayAItype.Return);
            }
        }
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
