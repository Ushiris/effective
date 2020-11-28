using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyState;

public class EnemyBrainShot : EnemyBrainBase
{
    private new void Start()
    {
        base.Start();
        FindAction = FindActionShot;
        Stay = StayActionShot;
        Think = ShotThink;
        enemyData.muzzleAngleLimit = new Vector3(360, 360, 360);
        IsLockAI = true;
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
        enemyData.MuzzleLookAt(player.transform.position);
    }

    void StayActionShot()
    {
        return;
    }
}
