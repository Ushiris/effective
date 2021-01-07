using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : EnemyBrainBase
{
    [SerializeField] SkinnedMeshRenderer changePoint;
    [SerializeField] int material_index;

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


    }
}
