using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyState;

public class EnemyBrainShot : EnemyBrainBase
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

        FindAction = FindActionShot;
        Stay = StayActionShot;
        Think = ShotThink;
        enemyData.muzzle.muzzleAngleLimit = new Vector3(360, 360, 360);
        IsLockAI = true;
    }

    private new void Update()
    {
        base.Update();
        enemyData.Aim();
    }

    void ShotThink()
    {
        switch (state.move)
        {
            case MoveState.Chase:
                FindAction();
                break;

            case MoveState.Stay:
                Stay();
                break;

            case MoveState.Confuse:
                Stay();
                break;

            default:
                DebugLogger.Log(gameObject.name + "「こういう時(" + state.ToString() + ")にどうすればいいのかわからん」");
                break;
        }

        EnchantAction();
    }

    void FindActionShot()
    {
    }

    void StayActionShot()
    {
    }
}
