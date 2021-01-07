using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrainTuibin : EnemyBrainBase
{
    PlayerManager playerManager;

    private new void Start()
    {
        base.Start();

        playerManager = PlayerManager.GetManager;

        AIset(FindAItype.Soldier);
        AIset(StayAItype.Ambush);
    }

    void StayMove()
    {
        navMesh.SetDestination(playerManager.tracePoint);
    }
}
