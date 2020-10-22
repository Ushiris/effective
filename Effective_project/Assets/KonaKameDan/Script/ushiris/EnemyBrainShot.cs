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
}
