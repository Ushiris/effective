using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrainTuibin : EnemyBrainBase
{
    [SerializeField] SkinnedMeshRenderer changePoint;
    [SerializeField] int material_index;

    PlayerManager playerManager;

    private new void Start()
    {
        mainMaterial = changePoint.materials[material_index];
        ApplyChangeColor = () =>
        {
            var temp = changePoint.materials;
            temp[material_index] = mainMaterial;
            changePoint.materials = temp;
        };
        base.Start();

        playerManager = PlayerManager.GetManager;
        AIset(FindAItype.Soldier);
        AIset(StayAItype.Ambush);
        Stay = StayMove;
    }

    void StayMove()
    {
        navMesh.SetDestination(playerManager.tracePoint);
    }
}
